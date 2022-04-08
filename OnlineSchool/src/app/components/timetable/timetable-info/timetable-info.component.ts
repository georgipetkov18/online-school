import { Component, OnDestroy, OnInit } from '@angular/core';
import { SignalRService } from 'src/app/services/signal-r.service';

@Component({
  selector: 'app-timetable-info',
  templateUrl: './timetable-info.component.html',
  styleUrls: ['./timetable-info.component.css']
})
export class TimetableInfoComponent implements OnInit, OnDestroy {

  constructor(private signalRService: SignalRService) { }

  ngOnInit(): void {
    this.signalRService.initiateConnection().then(_ => {
      this.signalRService.lessonBegan.subscribe({
        next: info => {
          console.log('lesson began');
          console.log(info);
        }
      });

      this.signalRService.lessonEnded.subscribe({
        next: () => {
          console.log('lesson ended');
        }
      });

      this.signalRService.lastLessonEnded.subscribe({
        next: () => {
          console.log('last lesson ended');
        }
      });

      this.signalRService.connection.invoke('GetData');
    })

  }

  ngOnDestroy(): void {
    this.signalRService.lessonBegan.unsubscribe();
    this.signalRService.lessonEnded.unsubscribe();
    this.signalRService.lastLessonEnded.unsubscribe();
  }

}
