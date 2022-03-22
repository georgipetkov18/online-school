import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

import { ClassesService } from '../../../services/classes.service';
import { UtilityService } from '../../../services/utility.service';

@Component({
  selector: 'app-create-class',
  templateUrl: './create-class.component.html',
  styleUrls: ['./create-class.component.css']
})
export class CreateClassComponent implements OnInit {
  @ViewChild('createClassForm') public classesForm!: NgForm;
  public errorMessage!: string;
  constructor(
    public utilityService: UtilityService,
    private classesService: ClassesService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    if (this.classesForm.invalid) {
      this.errorMessage = 'Всички полета са задължителни';
      return;
    }

    const name = this.classesForm.value['name'];

    this.classesService.addClass(name).subscribe({
      next: () => {
        this.toastr.success('Класът беше създаден успешно');
      },
      error: errorMessage => {
        this.errorMessage = errorMessage;
      }
    });
  }

}
