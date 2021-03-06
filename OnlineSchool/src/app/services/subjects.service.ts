import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { environment } from '../../environments/environment';
import { SubjectRequest } from '../models/request/subject-request.model';
import { SubjectResponse } from '../models/response/subject-response.model';

@Injectable({
  providedIn: 'root'
})
export class SubjectsService {

  constructor(private http: HttpClient) { }

  public getAllSubjects(filter: string) {
    return this.http.get<SubjectResponse[]>(environment.routes.subjects, {
      params: new HttpParams().append('filter', filter)
    }).pipe(tap(subjects => {
      return subjects.forEach(s => s.autoCompleteIdentifier = s.name)
    }));;
  }

  public addSubject(name: string, code: string) {
    const subjectRequest = new SubjectRequest(name, code);
    return this.http.post<SubjectResponse>(environment.routes.subjects + '/add', subjectRequest)
      .pipe(catchError(_ => {
        return throwError('Всички полета са задължителни');
      }));
  }

  public updateSubject(id: string, name: string, code: string) {
    const subjectRequest = new SubjectRequest(name, code);

    return this.http.put<SubjectResponse>(environment.routes.subjects + '/update' + `/${id}`, subjectRequest)
      .pipe(catchError(_ => {
        return throwError('Всички полета са задължителни');
      }));
  }

  public getSubject(id: string) {
    return this.http.get<SubjectResponse>(environment.routes.subjects + `/get/${id}`)
  }

  public deleteSubject(id: string) {
    return this.http.delete<void>(environment.routes.subjects + `/delete/${id}`);
  }
}
