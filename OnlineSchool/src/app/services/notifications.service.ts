import { Injectable } from '@angular/core';
import { TimetableEntryResponse } from '../models/response/timetable-entry-response.model';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor() { }

  public setNotification(entry: TimetableEntryResponse) {
    let [hour, minutes, seconds] = entry.from.split(':').map(s => +s);
    let now = new Date();
    let lessonStart = new Date(now.getFullYear(), now.getMonth(), now.getDate(), hour, minutes, seconds);
    let id = setTimeout(() => {
      let notification = new Notification('A Message From Code Picker!', {
        body: `Часът: ${entry.name} започва след 1 минута. Натиснете тук за да се присъедините.`,
      });
      notification.onclick = () => this.setMeetRedirect(entry.code);
    }, lessonStart.getTime() - now.getTime() - 1 * 60 * 1000);
    return id;
  }

  public copyToClipboard(text: string) {
    navigator.clipboard.writeText(text);
  }

  private setMeetRedirect(code: string) {
    let url = `https://meet.google.com/lookup/${code}`;
    window.open(url);
  }
}
