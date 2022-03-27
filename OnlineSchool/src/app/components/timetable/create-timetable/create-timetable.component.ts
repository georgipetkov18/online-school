import { Component, OnInit } from '@angular/core';
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

@Component({
  selector: 'app-create-timetable',
  templateUrl: './create-timetable.component.html',
  styleUrls: ['./create-timetable.component.css']
})
export class CreateTimetableComponent implements OnInit {
  public daysOfWeek = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
  public lessonsCount = 7;
  public suggestions: string[] = [];
  public lessonsArray: number[] = [];
  public timetable: (TimetableEntryRequest | null)[][] = [];
  public submitEnabled = false;
  private modalRef!: NgbModalRef;
  private currentRow = -1;
  private currentCol = -1;
  private suggestionsFull: AutoComplete[] = [];
  private ids = {
    subject: '',
    lesson: '',
    teacher: '',
  }

  constructor(
    private modalService: NgbModal,
    private subjectsService: SubjectsService,
    private lessonsService: LessonsService,
    private teachersService: TeachersService) { }

  ngOnInit(): void {
    this.lessonsArray = Array(this.lessonsCount).fill(0).map((_, i) => i);
    this.timetable = Array(this.lessonsCount).fill(null).map(_ =>
      Array(this.daysOfWeek.length).fill(null)
    );
  }

  addRow() {
    const nextNumber = this.lessonsArray[this.lessonsArray.length - 1] + 1;
    this.lessonsArray.push(nextNumber);
    this.updateTable();
  }

  openModal(content: any, row: number, col: number) {
    this.currentRow = row;
    this.currentCol = col;
    this.modalRef = this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
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

  onSubmitModal(modalForm: NgForm) {    
    // Get data from the form
    // Create timetable entry but do not send it to the backend
    // Save the returned output into this.timetable[this.currentRow][this.currentCol]
    // Close modal
    this.currentRow = -1;
    this.currentCol = -1;
  }

  private updateTable() {
    this.timetable.push(Array(this.daysOfWeek.length).fill(null));
  }

}
