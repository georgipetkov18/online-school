import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ClassesService } from 'src/app/services/classes.service';
import { IAppFormControl } from '../../shared/form/form.component';

@Component({
  selector: 'app-update-class',
  templateUrl: './update-class.component.html',
  styleUrls: ['./update-class.component.css']
})
export class UpdateClassComponent implements OnInit {
  public errorMessage!: string;
  private id!: string;
  public formSetup: IAppFormControl[] = [
    {
     name: 'name',
     label: 'Име *',
     initialValue:  this.route.snapshot.data[0].name,
     validators: [Validators.required, Validators.maxLength(30)]
    },
  ]

  constructor(
    private classesService: ClassesService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router) {}

  ngOnInit(): void {  
    this.id = this.route.snapshot.data[0].id;  
  }

  onSubmit(classesForm: FormGroup) {
    if (classesForm.invalid) {
      this.errorMessage = 'Всички полета са задължителни';
      return;
    }

    const name = classesForm.value['name'];

    this.classesService.updateClass(this.id, name).subscribe({
      next: () => {
        this.toastr.success('Класът беше променен успешно');
        this.router.navigate(['/', 'classes', 'all']);
      },
      error: errorMessage => {
        this.errorMessage = errorMessage;
      }
    });
  }

}
