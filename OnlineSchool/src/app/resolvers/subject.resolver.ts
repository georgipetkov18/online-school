import { Injectable } from '@angular/core';
import {
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable } from 'rxjs';
import { SubjectResponse } from '../models/response/subject-response.model';
import { SubjectsService } from '../services/subjects.service';

@Injectable({
  providedIn: 'root'
})
export class SubjectResolver implements Resolve<SubjectResponse> {
  constructor(private subjectsService: SubjectsService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<SubjectResponse> {
    const id = route.params['id'];
    return this.subjectsService.getSubject(id);
  }
}
