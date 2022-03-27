import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { TimetableEntryRequest } from 'src/app/models/request/timetable-entry-request.model';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

import { SubjectsService } from 'src/app/services/subjects.service';
import { SubjectResponse } from 'src/app/models/response/subject-response.model';

@Component({
  selector: 'app-create-timetable',
  templateUrl: './create-timetable.component.html',
  styleUrls: ['./create-timetable.component.css']
})
export class CreateTimetableComponent implements OnInit {
  public daysOfWeek = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
  public lessonsCount = 7;
  public subjectResponses: SubjectResponse[] = [];
  public suggestions: string[] = [];
  public lessonsArray: number[] = [];
  public timetable: (TimetableEntryRequest | null)[][] = [];
  private modalRef!: NgbModalRef;
  private currentRow = -1;
  private currentCol = -1;

  constructor(
    private modalService: NgbModal,
    private subjectsService: SubjectsService) { }

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
    switch (source) {
      case 'subjects':
        if (search.length < 3) {
          this.subjectResponses = [];
          this.suggestions = [];
          return;
        }
        this.subjectsService.getAllSubjects(search).subscribe({
          next: subjects => {
            this.subjectResponses = subjects;
            this.suggestions = subjects.map(s => s.name);
          }
        })
        break;

      case 'lessons':
        break;

      case 'teachers':
        break;
    }
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
