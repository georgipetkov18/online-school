import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';

import { TimetableEntryRequest } from 'src/app/models/request/timetable-entry-request.model';
import { SubjectsService } from 'src/app/services/subjects.service';
import { LessonsService } from 'src/app/services/lessons.service';
import { TeachersService } from 'src/app/services/teachers.service';
import { LessonResponse } from 'src/app/models/response/lesson-response.model';
import { SubjectResponse } from 'src/app/models/response/subject-response.model';
import { TeacherResponse } from 'src/app/models/response/teacher-response.model';
import { AutoComplete } from 'src/app/models/auto-complete.model';
import { ToastrService } from 'ngx-toastr';
import { TimetableValue } from 'src/app/models/timetable-value.model';
import { ClassResponse } from 'src/app/models/response/class-response.model';
import { ClassesService } from 'src/app/services/classes.service';
import { TimetableService } from 'src/app/services/timetable.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-create-timetable',
  templateUrl: './create-timetable.component.html',
  styleUrls: ['./create-timetable.component.css']
})
export class CreateTimetableComponent implements OnInit, AfterViewInit {
  @ViewChild('class') classModal!: any;
  public daysOfWeek: DayOfWeek[] = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
  public lessonsCount = 1;
  public suggestions: string[] = [];
  public lessonsArray: number[] = [];
  public timetable: (TimetableValue | null)[][] = [];
  public classes: ClassResponse[] = [];
  public classesNames: string[] = [];
  public submitEnabled = false;
  public submitClass = false;
  private infoModalRef!: NgbModalRef;
  private classModalRef!: NgbModalRef;
  private currentRow = -1;
  private currentCol = -1;
  private suggestionsFull: AutoComplete[] = [];
  public ids = {
    subject: '',
    lesson: '',
    teacher: '',
    class: '',
  }

  constructor(
    private modalService: NgbModal,
    private toastr: ToastrService,
    private subjectsService: SubjectsService,
    private lessonsService: LessonsService,
    private teachersService: TeachersService,
    private classesService: ClassesService,
    private timetableService: TimetableService,
    private router: Router,
    private location: Location) { }

  ngOnInit(): void {

  }

  ngAfterViewInit(): void {
    this.classModalRef = this.modalService.open(this.classModal, { ariaLabelledBy: 'modal-basic-title' });

    this.classModalRef.closed.subscribe(_ => {
      this.timetableService.getTimetableByClassId(this.ids.class).subscribe(timetable => {
        const formattedTimetable = this.timetableService.formatTableData(timetable);
        this.lessonsCount = formattedTimetable.length > 0 ? formattedTimetable.length : 1;
        if (formattedTimetable.length === 0) {
          this.lessonsCount = 1;
          this.lessonsArray = Array(this.lessonsCount).fill(0).map((_, i) => i);
          this.timetable = Array(this.lessonsCount).fill(null).map(_ =>
            Array(this.daysOfWeek.length).fill(null)
          );
          return;
        }

        this.lessonsCount = formattedTimetable.length;
        this.lessonsArray = Array(this.lessonsCount).fill(0).map((_, i) => i);
        this.timetable = Array(this.lessonsCount).fill(null).map(_ =>
          Array(this.daysOfWeek.length).fill(null)
        );
        this.timetable = formattedTimetable.map(row => {
          return row.map(el => el ?
            new TimetableValue([el.name, el.from, `${el.teacher.firstName} ${el.teacher.lastName}`], el) :
            null);
        })
      })
    });

    this.classModalRef.dismissed.subscribe(_ => {
      this.location.back();
    })
  }

  addRow() {
    const nextNumber = this.lessonsArray[this.lessonsArray.length - 1] + 1;
    this.lessonsArray.push(nextNumber);
    this.updateTable();
  }

  openInfoModal(content: any, row: number, col: number) {
    this.currentRow = row;
    this.currentCol = col;
    this.infoModalRef = this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  onFilterClasses(search: string) {
    if (search === '') {
      return;
    }
    this.classesService.getAllClasses(search).subscribe(classes => {
      this.classes = classes;
      this.classesNames = classes.map(c => c.name);
    })
  }

  onChangeSearch(source: 'subject' | 'lesson' | 'teacher', search: string) {
    let suggestionsObs: Observable<LessonResponse[] | SubjectResponse[] | TeacherResponse[]> = new Observable();

    switch (source) {
      case 'subject':
        if (search.length < 3) {
          this.suggestions = [];
          this.suggestionsFull = [];
          return;
        }
        suggestionsObs = this.subjectsService.getAllSubjects(search);
        break;

      case 'lesson':
        if (search === '') {
          this.suggestions = [];
          this.suggestionsFull = [];
          return;
        }
        suggestionsObs = this.lessonsService.getAllLessons(search);
        break;

      case 'teacher':
        if (search.length < 3) {
          this.suggestions = [];
          this.suggestionsFull = [];
          return;
        }
        suggestionsObs = this.teachersService.getAllTeachers(search);
        break;
    }
    suggestionsObs.subscribe({
      next: suggestions => {
        this.suggestionsFull = suggestions;
        this.suggestions = suggestions.map(s => s.autoCompleteIdentifier);
      }
    })
  }

  setClassId(value: string) {
    const currentElement = this.classes.find(s => s.name === value);

    if (!currentElement) {
      this.ids.class = '';
      this.submitEnabled = false;
      return;
    }
    this.ids.class = currentElement.id;
    console.log(this.ids);

  }

  setId(source: 'subject' | 'lesson' | 'teacher', value: string) {
    const currentElement = this.suggestionsFull.find(s => s.autoCompleteIdentifier === value);

    if (!currentElement) {
      this.ids[source] = '';
      this.submitEnabled = false;
      return;
    }
    this.ids[source] = currentElement.id;

    if (this.ids.lesson && this.ids.subject && this.ids.teacher) {
      this.submitEnabled = true;
    }
  }

  removeId(source: 'subject' | 'lesson' | 'teacher') {
    this.ids[source] = '';
    this.submitEnabled = false;
  }

  onFocus() {
    this.suggestions = [];
  }

  onSubmitClassModal(form: NgForm) {
    const classId = form.value['class'];
    this.setClassId(classId);
    this.classModalRef.close();
  }

  onSubmitModal(modalForm: NgForm) {
    if (modalForm.invalid) {
      this.toastr.error('Въведени са невалидни данни');
      return;
    }
    const subject = modalForm.value['modalSubject'];
    const lesson = modalForm.value['modalLesson'];
    const teacher = modalForm.value['modalTeacher'];
    const timetableEntry = new TimetableEntryRequest(
      this.daysOfWeek[this.currentCol],
      this.ids.subject,
      this.ids.lesson,
      this.ids.class,
      this.ids.teacher);

    const timetableValue = new TimetableValue([
      subject, lesson, teacher
    ], timetableEntry);

    this.timetable[this.currentRow][this.currentCol] = timetableValue;

    this.infoModalRef.close();
    this.currentRow = -1;
    this.currentCol = -1;
  }

  saveProgramme() {
    const entries: TimetableEntryRequest[] = [];
    for (let i = 0; i < this.timetable.length; i++) {
      const row = this.timetable[i];
      for (let j = 0; j < row.length; j++) {
        const value = row[j];
        if (value && value.entry instanceof TimetableEntryRequest) {
          value.entry.classId = this.ids.class;
          entries.push(value.entry);
        }
      }
    }

    this.timetableService.addTimetable(entries).subscribe({
      next: () => {
        this.toastr.success('Програмата беше създадена успешно');
        this.router.navigate(['/', 'timetable', 'create'])
      },
      error: errorMessage => {
        this.toastr.error(errorMessage);
      }
    })
  }

  private updateTable() {
    this.timetable.push(Array(this.daysOfWeek.length).fill(null));
  }

}

export type DayOfWeek = 'Monday' | 'Tuesday' | 'Wednesday' | 'Thursday' | 'Friday';
