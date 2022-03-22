import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from '../../environments/environment';
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

  public addSubject() {
    // /api/subjects/add
  }
}
