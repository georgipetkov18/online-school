import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SubjectResponse } from '../models/response/subject-response.model';

@Injectable({
  providedIn: 'root'
})
export class SubjectsService {

  constructor(private http: HttpClient) { }

  public getAllSubjects() {
    return this.http.get<SubjectResponse[]>('/api/subjects');
  }
}
