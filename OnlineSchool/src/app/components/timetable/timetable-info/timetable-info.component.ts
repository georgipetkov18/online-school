import { Component, OnDestroy, OnInit } from '@angular/core';
import { TeacherResponse } from 'src/app/models/response/teacher-response.model';
import { TimetableEntryResponse } from 'src/app/models/response/timetable-entry-response.model';
import { SignalRService } from 'src/app/services/signal-r.service';

@Component({
  selector: 'app-timetable-info',
  templateUrl: './timetable-info.component.html',
  styleUrls: ['./timetable-info.component.css']
})
export class TimetableInfoComponent implements OnInit, OnDestroy {

  public current: TimetableEntryResponse = {
    name: 'test current',
    code: 'my test current code',
    teacher: new TeacherResponse('4hy7tgurebv27', 'Teacher', 'Current'),
    class: '12e',
    from: '12:00:00',
    to: '13:00:00'
  };

  public next: TimetableEntryResponse = {
    name: 'test next',
    code: 'my test next code',
    teacher: new TeacherResponse('4hy7tgurebv27', 'Teacher', 'Next'),
    class: '12e',
    from: '16:00:00',
    to: '20:00:00'
  };

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
