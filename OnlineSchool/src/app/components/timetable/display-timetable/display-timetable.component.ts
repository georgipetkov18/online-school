import { Component, OnInit } from '@angular/core';
import { FullTimetable } from 'src/app/models/full-timetable.model';
import { TimetableService } from 'src/app/services/timetable.service';

@Component({
  selector: 'app-display-timetable',
  templateUrl: './display-timetable.component.html',
  styleUrls: ['./display-timetable.component.css']
})
export class DisplayTimetableComponent implements OnInit {
  public timetable!: FullTimetable;
  public rows: number[] = [];

  constructor(private timetableService: TimetableService) { }

  ngOnInit(): void {
    // Try catch this
    this.timetableService.getTimetable().subscribe(timetable => {
      this.timetable = timetable;
      console.log(this.timetable);
      
      const maxValue = Object.values(this.timetable)
        .map(t => t.length)
        .reduce((acc, x) => x > acc ? x : acc);

      this.rows = Array(maxValue).fill(0).map((_, i) => i);
      
    })
  }

}