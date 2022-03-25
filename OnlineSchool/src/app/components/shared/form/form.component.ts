import { Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';

import { UtilityService } from '../../../services/utility.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  @Input() formControlsSetup: IAppFormControl[] = [];
  @Input() beforeInputs!: TemplateRef<any>;
  @Input() afterInputs!: TemplateRef<any>;
  @Input() errorMessage: string = '';
  @Input() submitBtnText: string = 'Изпрати';
  @Input() afterInputsContext: any;
  @Output() formSubmitted = new EventEmitter<FormGroup>();

  public form!: FormGroup;
  public formControls: AppFormControl[] = [];

  constructor(public utilityService: UtilityService) { }

  ngOnInit(): void {
    this.formControls = this.formControlsSetup.map(c => {
      return new AppFormControl(
        c.name,
        c.label,
        new FormControl(),
        c.inputType ? c.inputType : 'text'
      )
    })

    const formGroupInit: { [key: string]: AbstractControl } = {};
    this.formControls.forEach(o => {
      formGroupInit[o.name] = o.formControl;
    })
    this.form = new FormGroup(formGroupInit);
  }

  onSubmit() {
    this.formSubmitted.emit(this.form);
  }

}

export interface IAppFormControl {
  name: string,
  label: string,
  inputType?: string,
}

class AppFormControl implements IAppFormControl {
  constructor(
    public name: string,
    public label: string,
    public formControl: FormControl,
    public inputType: string
  ) { }
}
