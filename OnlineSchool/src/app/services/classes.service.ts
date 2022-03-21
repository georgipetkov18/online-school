import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClassResponse } from '../models/response/class-response.model';

@Injectable({
  providedIn: 'root'
})
export class ClassesService {

  constructor(private http: HttpClient) { }

  public getAllClasses(filter: string) {
    return this.http.get<ClassResponse[]>('/api/classes', 
    {
      params: new HttpParams().append('filter', filter)
    });
  }
}
