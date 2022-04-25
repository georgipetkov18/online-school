import { Injectable } from '@angular/core';
import {
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable } from 'rxjs';
import { LessonResponse } from '../models/response/lesson-response.model';
import { LessonsService } from '../services/lessons.service';

@Injectable({
  providedIn: 'root'
})
export class LessonResolver implements Resolve<LessonResponse> {
  constructor(private lessonsService: LessonsService) { }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<LessonResponse> {
    const id = route.params['id'];
    return this.lessonsService.getLesson(id);
  }
}
