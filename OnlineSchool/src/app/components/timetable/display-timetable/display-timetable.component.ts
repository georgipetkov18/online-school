import { Component, OnInit } from '@angular/core';
import { TimetableService } from 'src/app/services/timetable.service';

@Component({
  selector: 'app-display-timetable',
  templateUrl: './display-timetable.component.html',
  styleUrls: ['./display-timetable.component.css']
})
export class DisplayTimetableComponent implements OnInit {

  constructor(private timetableService: TimetableService) { }

  ngOnInit(): void {
    // Try catch this
    this.timetableService.getTimetable().subscribe(timetable => console.log(timetable))
  }

}