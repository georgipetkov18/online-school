import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { AutoComplete } from '../models/auto-complete.model';
import { Lesson } from '../models/lesson.model';

@Injectable({
  providedIn: 'root'
})
export class LessonsService {

  constructor(private http: HttpClient) { }

  public addLesson(from: string, duration: number) {
    const lesson = new Lesson(from, duration);
    return this.http.post<Lesson>(environment.routes.lessons + '/add', lesson).pipe(
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
    return this.http.get<Lesson[]>(environment.routes.lessons, {
      params: new HttpParams().append('filter', filter)
    }).pipe(tap(lessons => {
      return lessons.forEach(l => l.autoCompleteIdentifier = l.from)
    }));
  }
}
