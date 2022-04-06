import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection, IHttpConnectionOptions } from '@microsoft/signalr';
import { Subject } from 'rxjs';

import { TimetableEntriesInfo } from '../models/response/timetable-entries-info-response.model';
import { UsersService } from './users.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public connection!: HubConnection;
  // public hubHelloMessage = new BehaviorSubject<string | null>(null);
  public lessonBegan = new Subject<TimetableEntriesInfo>();
  public lessonEnded = new Subject<void>();
  public lastLessonEnded = new Subject<void>();

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
          console.log(`SignalR connection success! connectionId: ${this.connection.connectionId} `);
          resolve();
        })
        .catch((error) => {
          console.log(`SignalR connection error: ${error}`);
          reject();
        });
    });
  }

  private setClientMethods(): void {
    this.connection.on('LessonBegan', (info: TimetableEntriesInfo) => {
      this.lessonBegan.next(info);
    });

    this.connection.on('LessonEnded', () => {
      this.lessonEnded.next();
    });

    this.connection.on('LastLessonEnded', () => {
      this.lastLessonEnded.next();
    });

    this.connection.on('NoLessons', () => {
      console.log('no lessons');
    });

    this.connection.on('WaitingForLesson', (next) => {
      console.log(next);
    });
  }

}
