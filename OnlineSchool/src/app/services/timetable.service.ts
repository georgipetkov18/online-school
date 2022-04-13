import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { FullTimetable } from '../models/full-timetable.model';
import { TimetableEntryRequest } from '../models/request/timetable-entry-request.model';
import { TimetableEntryResponse } from '../models/response/timetable-entry-response.model';
import { HourPipe } from '../pipes/hour.pipe';

@Injectable({
  providedIn: 'root'
})
export class TimetableService {
  private dayIndex = new Map([
    ['monday', 0],
    ['tuesday', 1],
    ['wednesday', 2],
    ['thursday', 3],
    ['friday', 4],
  ]);

  constructor(private http: HttpClient, private hourPipe: HourPipe) { }

  public addTimetable(entries: TimetableEntryRequest[]) {
    return this.http.post<void>(`${environment.routes.timetable}/add`, { entries }).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = 'Нещо се обърка';
        if (error.status === 401) {
          errorMessage = 'Небходимо е да влезете в системата';
        }
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
    return this.http.get<FullTimetable>(environment.routes.timetable + '/Full');
  }

  public getTimetableByClassId(classId: string) {
    return this.http.get<FullTimetable>(`${environment.routes.timetable}/Full/${classId}`);
  }

  public formatTableData(timetable: FullTimetable) {
    let entries: TimetableEntryResponse[][] = [];
    entries = Array(this.dayIndex.size).fill([]);

    Object.entries(timetable).forEach(pair => {
      const dayEntries: TimetableEntryResponse[] = pair[1];
      dayEntries.forEach((el: TimetableEntryResponse) => {
        const transformed = this.hourPipe.transform(el.from);
        if (transformed) {
          el.from = transformed;
        }
      });
      const index = this.dayIndex.get(pair[0].toLowerCase());
      if (index !== undefined) {
        entries.splice(index, 1, dayEntries);
      }
    })


    // // Get get the values of the returned object which are arrays and get the lenght of the longest one
    // const maxValue = Object.values(timetable)
    //   .map(t => t.length)
    //   .reduce((acc, x) => x > acc ? x : acc, [[]]);

    // for (let i = 0; i < maxValue; i++) {
    //   const array = Array(this.dayIndex.size).fill(undefined);
    //   entries.push(array);
    // }

    // for (let i = 0; i < maxValue; i++) {
    //   Object.entries(timetable).forEach(pair => {
    //     const col = this.dayIndex.get(pair[0].toLowerCase());
    //     if (col !== undefined) {
    //       const value: TimetableEntryResponse[] = pair[1];
    //       const current = value[i];
    //       if (current) {
    //         const transformed = this.hourPipe.transform(current.from);
    //         if (transformed) {
    //           current.from = transformed;
    //         }
    //         entries[i].splice(col, 1, current);
    //       }
    //     }
    //   })
    // }
    return entries;
  }
}