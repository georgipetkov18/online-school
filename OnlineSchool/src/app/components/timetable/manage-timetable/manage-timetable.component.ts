import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Location } from '@angular/common';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

import { TimetableEntryRequest } from 'src/app/models/request/timetable-entry-request.model';
import { SubjectsService } from 'src/app/services/subjects.service';
import { LessonsService } from 'src/app/services/lessons.service';
import { TeachersService } from 'src/app/services/teachers.service';
import { LessonResponse } from 'src/app/models/response/lesson-response.model';
import { SubjectResponse } from 'src/app/models/response/subject-response.model';
import { TeacherResponse } from 'src/app/models/response/teacher-response.model';
import { AutoComplete } from 'src/app/models/auto-complete.model';
import { TimetableValue } from 'src/app/models/timetable-value.model';
import { ClassResponse } from 'src/app/models/response/class-response.model';
import { ClassesService } from 'src/app/services/classes.service';
import { TimetableService } from 'src/app/services/timetable.service';
import { FullTimetable } from 'src/app/models/full-timetable.model';
import { TimetableEntryResponse } from 'src/app/models/response/timetable-entry-response.model';

@Component({
  selector: 'app-manage-timetable',
  templateUrl: './manage-timetable.component.html',
  styleUrls: ['./manage-timetable.component.css']
})
export class ManageTimetableComponent implements AfterViewInit {
  @ViewChild('class') classModal!: any;
  @ViewChild('subjectAutoComplete') subjectAutoComplete!: any;
  @ViewChild('lessonAutoComplete') lessonAutoComplete!: any;
  @ViewChild('teacherAutoComplete') teacherAutoComplete!: any;

  public daysOfWeek: DayOfWeek[] = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
  public lessonsCount = 1;
  public suggestions: string[] = [];
  public lessonsArray: number[] = [];
  public unformattedTimetable!: FullTimetable;
  public timetable: (TimetableValue | null)[][] = [];
  public classes: ClassResponse[] = [];
  public classesNames: string[] = [];
  public submitEnabled = false;
  public submitClass = false;
  public updateMode = false;
  private infoModalRef!: NgbModalRef;
  private classModalRef!: NgbModalRef;
  private currentRow = -1;
  private currentCol = -1;
  private suggestionsFull: AutoComplete[] = [];
  private updateValues: {subject: string, lesson: string, teacher: string} = {subject: '', lesson: '', teacher:''};
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

  ngAfterViewInit(): void {
    this.classModalRef = this.modalService.open(this.classModal, { ariaLabelledBy: 'modal-basic-title' });
    this.classModalRef.closed.subscribe(_ => {
      this.timetableService.getTimetableByClassId(this.ids.class).subscribe(timetable => {
        const formattedTimetable = this.timetableService.formatTableData(timetable);
        
        this.lessonsCount = formattedTimetable.length > 0 ? formattedTimetable.length : 1;
        this.lessonsArray = Array(this.lessonsCount).fill(0).map((_, i) => i);
        this.timetable = Array(this.lessonsCount).fill(null).map(_ =>
          Array(this.daysOfWeek.length).fill(null)
        );
        if (formattedTimetable.length === 0) {
          return;
        }
        const maxLength = Math.max(...formattedTimetable.map(x => x.length))
        for (let i = 0; i < formattedTimetable.length; i++) {
          const row = formattedTimetable[i];
          for (let j = 0; j < maxLength; j++) {
            const element = row[j];
            this.timetable[i][j] = element ?
              new TimetableValue([element.name, element.from,
              `${element.teacher.firstName} ${element.teacher.lastName}`], element) :
              null
          }
        }
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

  openInfoModal(content: any, row: number, col: number, updateMode = false, args: string[] = []) {
    this.currentRow = row;
    this.currentCol = col;
    this.updateMode = updateMode;
    this.infoModalRef = this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
    if (updateMode) {
      this.infoModalRef.shown.subscribe(_ => {
        const subjectInput = document.querySelector('#modal-subject .input-container input') as HTMLInputElement;
        const lessoninput = document.querySelector('#modal-lesson .input-container input') as HTMLInputElement;
        const teacherInput = document.querySelector('#modal-teacher .input-container input') as HTMLInputElement;
        subjectInput.value = args[0];
        lessoninput.value = args[1];
        teacherInput.value = args[2];
        this.updateValues.subject = args[0];
        this.updateValues.lesson = args[1];
        this.updateValues.teacher = args[2];
      })
    }
  }

  onFilterClasses(search: string) {
    if (search === '') {
      return;
    }
    this.classesService.getAllClasses(search).subscribe(classes => {
      this.classes = classes;
      this.classesNames = classes.map(c => c.name);
    })
    this.submitClass = this.classes.map(c => c.name).includes(search)
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
        if (this.suggestions.includes(search)) {
          this.setId(source, search);
        }
        else {
          this.submitEnabled = false;
        }
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
  }

  setId(source: 'subject' | 'lesson' | 'teacher', value: string) {
    const currentElement = this.suggestionsFull.find(s => s.autoCompleteIdentifier === value);
    
    if (!currentElement) {
      this.ids[source] = '';
      this.submitEnabled = false;
      return;
    }
    this.ids[source] = currentElement.id;
    
    if ((this.ids.lesson && this.ids.subject && this.ids.teacher) || this.updateMode) {
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
    
    const subject = modalForm.value['modalSubject'] || this.updateValues.subject;
    const lesson = modalForm.value['modalLesson'] || this.updateValues.lesson;
    const teacher = modalForm.value['modalTeacher'] || this.updateValues.teacher;
    if (this.updateMode) {
      this.updateEntry(subject, lesson, teacher);
    }
    else {
      this.createEntry(subject, lesson, teacher);
    }

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

  onDelete(row: number, col: number) {
    const value = this.timetable[row][col];
    if (value) {
      const entry = value.entry;
      if (entry instanceof TimetableEntryRequest) {
        this.timetable[row][col] = null;
        this.toastr.success('Часът беше изтрит успешно');
      }
      else {
        const entryId = (<TimetableEntryResponse>entry).timetableEntryId;
        this.timetableService.deleteEntry(entryId).subscribe(() => {
          this.timetable[row][col] = null;
          this.toastr.success('Часът беше изтрит успешно');
        });
      }
    }
  }

  createEntry(subject: string, lesson: string, teacher: string) {
    const timetableEntry = new TimetableEntryRequest(
      this.daysOfWeek[this.currentRow],
      this.ids.subject,
      this.ids.lesson,
      this.ids.class,
      this.ids.teacher);

    const timetableValue = new TimetableValue([
      subject, lesson, teacher
    ], timetableEntry);

    this.timetable[this.currentRow][this.currentCol] = timetableValue;
  }

  updateEntry(subject: string, lesson: string, teacher: string) {
    const entry = this.timetable[this.currentRow][this.currentCol]?.entry;
    if (entry instanceof TimetableEntryRequest) {
      this.createEntry(subject, lesson, teacher);
    }
    else {
      const entryId = (<TimetableEntryResponse>entry).timetableEntryId;
      const timetableEntry = new TimetableEntryRequest(
        this.daysOfWeek[this.currentRow],
        this.ids.subject,
        this.ids.lesson,
        this.ids.class,
        this.ids.teacher,
        entryId);

      const timetableValue = new TimetableValue([
        subject, lesson, teacher
      ], timetableEntry);

      this.timetable[this.currentRow][this.currentCol] = timetableValue;
    }
  }

  onAddNewLesson(form: NgForm) {
    const lessoninput = document.querySelector('#modal-lesson .input-container input') as HTMLInputElement;
    const splitted = lessoninput.value.split(':').filter(s => s !== '').map(s => +s);

    if (splitted.length < 2 || Number.isNaN(splitted[0]) || Number.isNaN(splitted[1])) {
      this.toastr.error('Въведеният час е невалиден');
      return;
    }
    else if (form.invalid) {
      this.toastr.error('Полето "Продължителност" е невалидно');
      return;
    }
    const durationInMinutes = +form.value['dynamicDuration'];
    const from = lessoninput.value + ':00';
    this.lessonsService.addLesson(from, durationInMinutes).subscribe({
      next: lesson => {
        this.suggestions.push(lesson.autoCompleteIdentifier);
        this.suggestionsFull.push(lesson);
        this.setId('lesson', lesson.autoCompleteIdentifier);
        this.toastr.success('Часът е създаден успешно');
      },
      error: errorMessage => {
        this.toastr.error(errorMessage);
      }
    })

  }

  private updateTable() {
    this.timetable.forEach(row => {
      row.push(null);
    });
  }

}

export type DayOfWeek = 'Monday' | 'Tuesday' | 'Wednesday' | 'Thursday' | 'Friday';
