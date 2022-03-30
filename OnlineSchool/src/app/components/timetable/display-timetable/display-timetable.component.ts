import { Component, OnInit } from '@angular/core';
import { FullTimetable } from 'src/app/models/full-timetable.model';
import { TimetableEntryResponse } from 'src/app/models/response/timetable-entry-response.model';
import { TimetableService } from 'src/app/services/timetable.service';
import { DayOfWeek } from '../create-timetable/create-timetable.component';

@Component({
  selector: 'app-display-timetable',
  templateUrl: './display-timetable.component.html',
  styleUrls: ['./display-timetable.component.css']
})
export class DisplayTimetableComponent implements OnInit {
  public daysOfWeek: DayOfWeek[] = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
  public data: TimetableEntryResponse[][] = [];

  constructor(private timetableService: TimetableService) { }

  ngOnInit(): void {
    // Try catch this
    this.timetableService.getTimetable().subscribe(timetable => {
      this.data = this.timetableService.formatTableData(timetable);
    })
  }
}