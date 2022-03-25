import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { environment } from '../../environments/environment';
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
}
