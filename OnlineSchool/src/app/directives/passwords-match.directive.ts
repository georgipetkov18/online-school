import { Directive, Input } from '@angular/core';
import { AbstractControl, NgModel, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';

@Directive({
  selector: '[appPasswordsMatch]',
  providers: [{provide: NG_VALIDATORS, useExisting: PasswordsMatchDirective, multi: true}]
})
export class PasswordsMatchDirective implements Validator {
  @Input('appPasswordsMatch') matchingField!: NgModel;
  constructor() { }

  validate(control: AbstractControl): ValidationErrors | null {    
    return control.value !== this.matchingField.value ? {
      passwordsDiffer: true
    } : null
  }

}
