import { Component, OnInit } from '@angular/core';
import { TimetableEntryResponse } from 'src/app/models/response/timetable-entry-response.model';
import { TimetableService } from 'src/app/services/timetable.service';
import { DayOfWeek } from '../manage-timetable/manage-timetable.component';

@Component({
  selector: 'app-display-timetable',
  templateUrl: './display-timetable.component.html',
  styleUrls: ['./display-timetable.component.css']
})
export class DisplayTimetableComponent implements OnInit {
  public data: TimetableEntryResponse[][] = [];
  public daysOfWeek: DayOfWeek[] = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];

  constructor(private timetableService: TimetableService) { }

  ngOnInit(): void {
    this.timetableService.getTimetable().subscribe(timetable => {
      this.data = this.timetableService.formatTableData(timetable);      
    })
  }
}