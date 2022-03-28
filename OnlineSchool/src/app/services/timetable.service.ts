import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { TimetableEntryRequest } from '../models/request/timetable-entry-request.model';

@Injectable({
  providedIn: 'root'
})
export class TimetableService {

  constructor(private http: HttpClient) { }

  public addTimetable(entries: TimetableEntryRequest[]) {
    return this.http.post<void>(`${environment.routes.timetable}/add`, { entries }).pipe(
      catchError(error => {
        console.log(error);
        
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
}
