import { Component, OnInit } from '@angular/core';
import { TimetableEntryRequest } from 'src/app/models/request/timetable-entry-request.model';

@Component({
  selector: 'app-create-timetable',
  templateUrl: './create-timetable.component.html',
  styleUrls: ['./create-timetable.component.css']
})
export class CreateTimetableComponent implements OnInit {
  public daysOfWeek = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
  public lessonsCount = 7;
  public lessonsArray: number[] = [];
  public filledTable: (TimetableEntryRequest | null)[][] = [];

  constructor() { }

  ngOnInit(): void {
    this.lessonsArray = Array(this.lessonsCount).fill(0).map((_, i) => i + 1);
    this.filledTable = Array(this.lessonsCount).fill(null).map(_ => Array(this.daysOfWeek.length).fill(null));
    console.log("init ", this.filledTable);
    
  }

  addRow() {
    const nextNumber = this.lessonsArray[this.lessonsArray.length - 1] + 1;
    this.lessonsArray.push(nextNumber);
    this.updateTable();
  }

  private updateTable() {
    this.filledTable.push(Array(this.daysOfWeek.length).fill(null));
    console.log("update ", this.filledTable);

  }

}
