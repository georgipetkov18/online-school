import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { DayOfWeek } from '../components/timetable/create-timetable/create-timetable.component';
import { FullTimetable } from '../models/full-timetable.model';
import { TimetableEntryRequest } from '../models/request/timetable-entry-request.model';
import { TimetableEntryResponse } from '../models/response/timetable-entry-response.model';
import { UsersService } from './users.service';

@Injectable({
  providedIn: 'root'
})
export class TimetableService {

  constructor(private http: HttpClient, private usersService: UsersService) { }

  public addTimetable(entries: TimetableEntryRequest[]) {
    const token = this.usersService.getCurrentUserToken();
    return this.http.post<void>(`${environment.routes.timetable}/add`, { entries }, {
      headers: new HttpHeaders().append('Authorization', `Bearer ${token}`)
    }).pipe(
      catchError(error => {
        let errorMessage = 'Нещо се обърка';
        if (error && error.error && error.error.Input) {
          errorMessage = error.error.Input[0];
        }
        if (error && error.error && error.error.timetableInputModel) {
          errorMessage = 'Избери клас';
        }
        return throwError(errorMessage);
      })
    );
  }

  public getTimetable() {
    const token = this.usersService.getCurrentUserToken();
    return this.http.get<FullTimetable>(environment.routes.timetable + '/Full', {
      headers: new HttpHeaders().append('Authorization', `Bearer ${token}`)
    });
  }

  public formatTableData(timetable: FullTimetable) {
    const entries: TimetableEntryResponse[][] = [];

    // Get get the values of the returned object which are arrays and get the lenght of the longest one
    const maxValue = Object.values(timetable)
      .map(t => t.length)
      .reduce((acc, x) => x > acc ? x : acc);

    for (let i = 0; i < maxValue; i++) {
      entries.push([]);
      Object.values(timetable).forEach(value => {
        const current = value[i];
        if (current) {
          entries[i].push(current);
        }
      })
    }
    return entries;
  }
}