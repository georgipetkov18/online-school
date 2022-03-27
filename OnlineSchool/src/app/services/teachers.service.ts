import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { TeacherResponse } from '../models/response/teacher-response.model';

@Injectable({
  providedIn: 'root'
})
export class TeachersService {

  constructor(private http: HttpClient) { }

  public getAllTeachers(filter: string) {
    return this.http.get<TeacherResponse[]>(environment.routes.teachers, {
      params: new HttpParams().append('filter', filter)
    }).pipe(tap(teachers => {
      return teachers.forEach(t => t.autoCompleteIdentifier = `${t.firstName} ${t.lastName}`)
    }));
  }
}
