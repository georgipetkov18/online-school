import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

import { SubjectsService } from '../../../services/subjects.service';
import { UtilityService } from '../../../services/utility.service';
import { IAppFormControl } from '../../shared/form/form.component';

@Component({
  selector: 'app-create-subject',
  templateUrl: './create-subject.component.html',
  styleUrls: ['./create-subject.component.css']
})
export class CreateSubjectComponent implements OnInit {
  public errorMessage!: string;
  public formSetup: IAppFormControl[] = [
    {
     name: 'name',
     label: 'Име *' 
    },
    {
      name: 'code',
      label: 'Код *' 
     },
  ]

  constructor(
    public utilityService: UtilityService,
    private subjectsService: SubjectsService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(subjectsForm: FormGroup) {
    if (subjectsForm.invalid) {
      this.errorMessage = 'Всички полета са задължителни';
      return;
    }

    const name = subjectsForm.value['name'];
    const code = subjectsForm.value['code'];

    this.subjectsService.addSubject(name, code).subscribe({
      next: () => {
        this.toastr.success('Успешно добавихте предмет');
      },
      error: errorMessage => {
        this.errorMessage = errorMessage;
      }
    });
  }

}
