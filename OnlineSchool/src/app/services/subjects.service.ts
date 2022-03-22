import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { SubjectRequest } from '../models/request/subject-request.model';
import { SubjectResponse } from '../models/response/subject-response.model';

@Injectable({
  providedIn: 'root'
})
export class SubjectsService {

  constructor(private http: HttpClient) { }

  public getAllSubjects(filter: string) {
    return this.http.get<SubjectResponse[]>(environment.routes.subjects,
      {
        params: new HttpParams().append('filter', filter)
      });
  }

  public addSubject(name: string, code: string) {
    const subjectRequest = new SubjectRequest(name, code);
    return this.http.post<SubjectResponse>(environment.routes.subjects + '/add', subjectRequest)
      .pipe(catchError(_ => {
        return throwError('Всички полета са задължителни');
      }));
  }
}
