import { Injectable, OnInit } from '@angular/core';
import { TimetableEntryResponse } from '../models/response/timetable-entry-response.model';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {
  private permission: 'granted' | 'denied' | 'default' = 'default';
  
  constructor() { }

  public requestPermission() {
    Notification.requestPermission().then(permission => {      
      this.permission = permission;
    })
  }

  public setNotification(entry: TimetableEntryResponse) {
    let [hour, minutes, seconds] = entry.from.split(':').map(s => +s);
    let now = new Date();
    let lessonStart = new Date(now.getFullYear(), now.getMonth(), now.getDate(), hour, minutes, seconds);
    let id = setTimeout(() => {
        if (this.permission === 'granted') {
          let notification = new Notification('Online School', {
            body: `Часът: ${entry.name} ще започне скоро. Натиснете тук за да се присъедините към срещата.`,
          });          
          notification.onclick = () => this.redirectToMeet(entry.code);
        }
    }, lessonStart.getTime() - now.getTime() - 1 * 60 * 1000);
    return id;
  }

  public copyToClipboard(text: string) {
    navigator.clipboard.writeText(text);
  }

  public redirectToMeet(code: string) {
    let url = `https://meet.google.com/lookup/${code}`;
    window.open(url);
  }
}
