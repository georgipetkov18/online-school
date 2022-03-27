import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';

import { TimetableEntryRequest } from 'src/app/models/request/timetable-entry-request.model';
import { SubjectsService } from 'src/app/services/subjects.service';
import { SubjectResponse } from 'src/app/models/response/subject-response.model';
import { LessonsService } from 'src/app/services/lessons.service';
import { TeachersService } from 'src/app/services/teachers.service';
import { TeacherResponse } from 'src/app/models/response/teacher-response.model';
import { Lesson } from 'src/app/models/lesson.model';
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
  private modalRef!: NgbModalRef;
  private currentRow = -1;
  private currentCol = -1;

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

  onChangeSearch(source: 'subjects' | 'lessons' | 'teachers', search: string) {
    let suggestionsObs: Observable<AutoComplete[]> = new Observable();

    switch (source) {
      case 'subjects':
        if (search.length < 3) {
          this.suggestions = [];
          return;
        }
        suggestionsObs = this.subjectsService.getAllSubjects(search);
        break;

      case 'lessons':
        if (search === '') {
          this.suggestions = [];
          return;
        }
        suggestionsObs = this.lessonsService.getAllLessons(search);
        break;

      case 'teachers':
        if (search.length < 3) {
          this.suggestions = [];
          return;
        }
        suggestionsObs = this.teachersService.getAllTeachers(search);
        break;
    }
    suggestionsObs.subscribe({
      next: suggestions => {
        console.log(suggestions);
        
        this.suggestions = suggestions.map(s => s.autoCompleteIdentifier);
        console.log(this.suggestions);
        
      }
    })
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
