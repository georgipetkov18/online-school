import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ClassResponse } from '../models/response/class-response.model';
import { environment } from '../../environments/environment';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { ClassRequest } from '../models/request/class-request.model';

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

  public addClass(name: string) {
    const classRequest = new ClassRequest(name);
    return this.http.post<ClassResponse>(environment.routes.classes + '/add', classRequest)
      .pipe(catchError(_ => {
        return throwError('Всички полета са задължителни');
      }));
  }
}
