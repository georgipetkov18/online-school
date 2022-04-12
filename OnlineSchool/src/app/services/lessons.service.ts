import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { LessonRequest } from '../models/request/lesson-request.model';
import { LessonResponse } from '../models/response/lesson-response.model';
import { HourPipe } from '../pipes/hour.pipe';

@Injectable({
  providedIn: 'root'
})
export class LessonsService {

  constructor(
    private http: HttpClient,
    private hourPipe: HourPipe) { }

  public addLesson(from: string, duration: number) {
    const lesson = new LessonRequest(from, duration);
    return this.http.post<LessonResponse>(environment.routes.lessons + '/add', lesson).pipe(
      catchError(error => {
        let errorMessage = 'Нещо се обърка';
        if (error && error.error && error.error.DurationInMinutes) {
          errorMessage = 'Полето минути трябва да съдържа число между 5 и 100';
        }
        if (error && error.error && error.error.LessonExists) {
          errorMessage = error.error.LessonExists[0];
        }
        return throwError(errorMessage);
      })
    );
  }

  public getAllLessons(filter: string) {
    return this.http.get<LessonResponse[]>(environment.routes.lessons, {
      params: new HttpParams().append('filter', filter)
    }).pipe(tap(lessons => {
      return lessons.forEach(l => {
        const transformed = this.hourPipe.transform(l.from);
        if (transformed) {
          l.from = transformed;
        }
        l.autoCompleteIdentifier = l.from;
      })
    }));
  }
}
