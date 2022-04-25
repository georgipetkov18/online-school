import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable } from 'rxjs';
import { ClassResponse } from '../models/response/class-response.model';
import { ClassesService } from '../services/classes.service';

@Injectable({
  providedIn: 'root'
})
export class ClassResolver implements Resolve<ClassResponse> {

  constructor(private classesService: ClassesService) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ClassResponse> {
    const id = route.params['id'];
    return this.classesService.getClass(id);
  }
}
