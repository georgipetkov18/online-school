import { Injectable } from '@angular/core';
import { NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {

  constructor() { }

  public resetForm(form: NgForm) {
    form.reset();
  }
}
