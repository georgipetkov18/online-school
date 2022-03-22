import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ClassResponse } from '../models/response/class-response.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClassesService {

  constructor(private http: HttpClient) { }

  public getAllClasses(filter: string) {
    return this.http.get<ClassResponse[]>(environment.routes.classes, 
    {
      params: new HttpParams().append('filter', filter)
    });
  }
}
