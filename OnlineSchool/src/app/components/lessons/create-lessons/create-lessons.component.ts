import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

import { LessonsService } from '../../../services/lessons.service';
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

  constructor(private toastr: ToastrService, private lessonsService: LessonsService) { }

  ngOnInit(): void {
  }

  onSubmit(lessonsForm: FormGroup) {
    if (lessonsForm.invalid) {
      this.toastr.error('Всички полета са задължителни');
      return;
    }
    const from = lessonsForm.value['from'] + ':00';
    const durationInMinutes = +lessonsForm.value['duration'];
    this.lessonsService.addLesson(from, durationInMinutes).subscribe({
      next: lesson => {
        this.toastr.success('Часът е създаден успешно');
        console.log(lesson);
      },
      error: errorMessage => {
        this.toastr.error(errorMessage);
      }
    })
  }

}
