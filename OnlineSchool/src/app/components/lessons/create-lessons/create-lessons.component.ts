import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { IAppFormControl } from '../../shared/form/form.component';

@Component({
  selector: 'app-create-lessons',
  templateUrl: './create-lessons.component.html',
  styleUrls: ['./create-lessons.component.css']
})
export class CreateLessonsComponent implements OnInit {
  public errorMessage!: string;
  public formSetup: IAppFormControl[] = [
    {
     name: 'from',
     label: 'От *' ,
     inputType: 'time'
    },
    {
      name: 'duration',
      label: 'Минути *',
      inputType: 'number'
    }
  ]

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit(lessonsForm: FormGroup) {

  }

}
