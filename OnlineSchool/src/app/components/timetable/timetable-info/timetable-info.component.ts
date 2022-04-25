import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
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
  private lessonBeganSub!: Subscription;
  private lessonEndedSub!: Subscription;
  private waitingForLessonSub!: Subscription;
  private lastLessonEndedSub!: Subscription;
  private noLessonsSub!: Subscription;

  constructor(
    private signalRService: SignalRService, 
    public notificationsService: NotificationsService) { }

  ngOnInit(): void {
    this.signalRService.initiateConnection().then(_ => {
      this.lessonBeganSub = this.signalRService.lessonBegan.subscribe({
        next: info => {          
          console.log(info);
          
          this.lastLessonEnded = false;
          this.current = info.current;
          this.next = info.next;
        }
      });

      this.lessonEndedSub = this.signalRService.lessonEnded.subscribe({
        next: nextLesson => {
          this.current = undefined;
          this.next = nextLesson;
          this.timeoutId = this.notificationsService.setNotification(nextLesson);
        }
      });

      this.waitingForLessonSub = this.signalRService.waitingForLesson.subscribe({
        next: nextLesson => {
          this.current = undefined;
          this.next = nextLesson;
          this.timeoutId = this.notificationsService.setNotification(nextLesson);
        }
      });

      this.lastLessonEndedSub = this.signalRService.lastLessonEnded.subscribe({
        next: () => {
          this.lastLessonEnded = true;
        }
      });

      this.noLessonsSub = this.signalRService.noLessons.subscribe({
        next: () => {
          this.lastLessonEnded = true;
        }
      });

      this.signalRService.connection.invoke('GetData');
      this.notificationsService.requestPermission();
    })

  }

  ngOnDestroy(): void {
    this.lessonBeganSub.unsubscribe();
    this.lessonEndedSub.unsubscribe();
    this.lastLessonEndedSub.unsubscribe();
    this.waitingForLessonSub.unsubscribe();
    this.noLessonsSub.unsubscribe();
    clearTimeout(this.timeoutId);
  }

}
