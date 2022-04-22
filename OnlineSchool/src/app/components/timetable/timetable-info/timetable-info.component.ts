import { Component, OnDestroy, OnInit } from '@angular/core';
import { TimetableEntryResponse } from 'src/app/models/response/timetable-entry-response.model';
import { NotificationsService } from 'src/app/services/notifications.service';
import { SignalRService } from 'src/app/services/signal-r.service';

@Component({
  selector: 'app-timetable-info',
  templateUrl: './timetable-info.component.html',
  styleUrls: ['./timetable-info.component.css']
})
export class TimetableInfoComponent implements OnInit, OnDestroy {
  public current: TimetableEntryResponse | undefined;
  public next: TimetableEntryResponse | undefined;
  public lastLessonEnded = false;
  private timeoutId: any;

  constructor(
    private signalRService: SignalRService, 
    public notificationsService: NotificationsService) { }

  ngOnInit(): void {
    this.signalRService.initiateConnection().then(_ => {
      this.signalRService.lessonBegan.subscribe({
        next: info => {          
          console.log(info);
          
          this.lastLessonEnded = false;
          this.current = info.current;
          this.next = info.next;
        }
      });

      this.signalRService.lessonEnded.subscribe({
        next: nextLesson => {
          this.current = undefined;
          this.next = nextLesson;
          this.timeoutId = this.notificationsService.setNotification(nextLesson);
        }
      });

      this.signalRService.waitingForLesson.subscribe({
        next: nextLesson => {
          this.current = undefined;
          this.next = nextLesson;
          this.timeoutId = this.notificationsService.setNotification(nextLesson);
        }
      });

      this.signalRService.lastLessonEnded.subscribe({
        next: () => {
          this.lastLessonEnded = true;
        }
      });

      this.signalRService.noLessons.subscribe({
        next: () => {
          
        }
      });

      this.signalRService.connection.invoke('GetData');
    })

  }

  ngOnDestroy(): void {
    this.signalRService.lessonBegan.unsubscribe();
    this.signalRService.lessonEnded.unsubscribe();
    this.signalRService.lastLessonEnded.unsubscribe();
    this.signalRService.waitingForLesson.unsubscribe();
    this.signalRService.noLessons.unsubscribe();
    clearTimeout(this.timeoutId);
  }

}
