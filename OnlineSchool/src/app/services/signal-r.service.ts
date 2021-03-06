import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection, IHttpConnectionOptions } from '@microsoft/signalr';
import { Subject } from 'rxjs';

import { TimetableEntriesInfo } from '../models/response/timetable-entries-info-response.model';
import { TimetableEntryResponse } from '../models/response/timetable-entry-response.model';
import { UsersService } from './users.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public connection!: HubConnection;
  public lessonBegan = new Subject<TimetableEntriesInfo>();
  public lessonEnded = new Subject<TimetableEntryResponse>();
  public waitingForLesson = new Subject<TimetableEntryResponse>();
  public lastLessonEnded = new Subject<void>();
  public noLessons = new Subject<void>();

  constructor(private usersService: UsersService) { }

  public initiateConnection(): Promise<void> {
    const token = this.usersService.getCurrentUserToken();
    if (token === null) {
      throw new Error('Необходим е вход в системата');
    }
    const options: IHttpConnectionOptions = {
      accessTokenFactory: () => {
        return token;
      }
    }
    return new Promise((resolve, reject) => {
      this.connection = new HubConnectionBuilder()
        .withUrl('/api/timetableHub', options)
        .build();

      this.setClientMethods();

      this.connection
        .start()
        .then(() => {
          resolve();
        })
        .catch((error) => {
          reject();
        });
    });
  }

  private setClientMethods(): void {
    this.connection.on('LessonBegan', (info: TimetableEntriesInfo) => {
      this.lessonBegan.next(info);
    });

    this.connection.on('LessonEnded', (next: TimetableEntryResponse) => {
      this.lessonEnded.next(next);
    });

    this.connection.on('LastLessonEnded', () => {
      this.lastLessonEnded.next();
    });

    this.connection.on('NoLessons', () => {
      this.noLessons.next();
    });

    this.connection.on('WaitingForLesson', (next: TimetableEntryResponse) => {
      this.waitingForLesson.next(next);
    });
  }

}
