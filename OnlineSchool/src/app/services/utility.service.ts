import { Injectable } from '@angular/core';
import { FormGroup, NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {

  constructor() { }

  public resetGroup(form: FormGroup) {
    form.reset();
  }

  public resetForm(form: NgForm) {
    form.reset();
  }
}
