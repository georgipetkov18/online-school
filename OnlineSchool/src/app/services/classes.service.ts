import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClassResponse } from '../models/response/class-response.model';

@Injectable({
  providedIn: 'root'
})
export class ClassesService {

  constructor(private http: HttpClient) { }

  public getAllClasses() {
    return this.http.get<ClassResponse[]>('/api/classes');
  }
}
