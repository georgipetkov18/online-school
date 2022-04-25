import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { ClassesService } from '../../../services/classes.service';
import { UtilityService } from '../../../services/utility.service';
import { IAppFormControl } from '../../shared/form/form.component';

@Component({
  selector: 'app-create-class',
  templateUrl: './create-class.component.html',
  styleUrls: ['./create-class.component.css']
})
export class CreateClassComponent implements OnInit {
  public errorMessage!: string;
  public formSetup: IAppFormControl[] = [
    {
     name: 'name',
     label: 'Име *',
     validators: [Validators.required, Validators.maxLength(30)]
    },
  ]

  constructor(
    public utilityService: UtilityService,
    private classesService: ClassesService,
    private toastr: ToastrService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
  }

  onSubmit(classesForm: FormGroup) {
    if (classesForm.invalid) {
      this.errorMessage = 'Всички полета са задължителни';
      return;
    }

    const name = classesForm.value['name'];

    this.classesService.addClass(name).subscribe({
      next: () => {
        this.toastr.success('Класът беше създаден успешно');
        this.router.navigate(['../', 'all'], {relativeTo: this.route});
      },
      error: errorMessage => {
        this.errorMessage = errorMessage;
      }
    });
  }

}
