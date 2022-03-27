import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { LessonRequest } from '../models/request/lesson-request.model';
import { LessonResponse } from '../models/response/lesson-response.model';

@Injectable({
  providedIn: 'root'
})
export class LessonsService {

  constructor(private http: HttpClient) { }

  public addLesson(from: string, duration: number) {
    const lesson = new LessonRequest(from, duration);
    return this.http.post<LessonResponse>(environment.routes.lessons + '/add', lesson).pipe(
      catchError(error => {
        let errorMessage = 'Нещо се обърка';
        if (error && error.error && error.error.DurationInMinutes) {
          errorMessage = 'Полето минути трябва да съдържа число между 5 и 100';
        }
        return throwError(errorMessage);
      })
    );
  }

  public getAllLessons(filter: string) {
    return this.http.get<LessonResponse[]>(environment.routes.lessons, {
      params: new HttpParams().append('filter', filter)
    }).pipe(tap(lessons => {
      return lessons.forEach(l => l.autoCompleteIdentifier = l.from)
    }));
  }
}
