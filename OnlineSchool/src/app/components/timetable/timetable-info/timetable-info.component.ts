import { Component, OnInit } from '@angular/core';
import { SignalRService } from 'src/app/services/signal-r.service';

@Component({
  selector: 'app-timetable-info',
  templateUrl: './timetable-info.component.html',
  styleUrls: ['./timetable-info.component.css']
})
export class TimetableInfoComponent implements OnInit {

  constructor(private signalRService: SignalRService) { }

  ngOnInit(): void {
    this.signalRService.initiateConnection().then(_ => {
      this.signalRService.lessonBegan.subscribe({
        next: info => {
          console.log(info);
        }
      });

      this.signalRService.lessonEnded.subscribe({
        next: () => {

        }
      });

      this.signalRService.lastLessonEnded.subscribe({
        next: () => {

        }
      });

      this.signalRService.connection.invoke('GetData');
    })

  }

}
