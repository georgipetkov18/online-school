import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SubjectsService } from 'src/app/services/subjects.service';
import { IAppFormControl } from '../../shared/form/form.component';

@Component({
  selector: 'app-update-subject',
  templateUrl: './update-subject.component.html',
  styleUrls: ['./update-subject.component.css']
})
export class UpdateSubjectComponent implements OnInit {
  public errorMessage!: string;
  private id!: string;
  public formSetup: IAppFormControl[] = [
    {
      name: 'name',
      label: 'Име *',
      initialValue: this.route.snapshot.data[0].name,
      validators: [Validators.required, Validators.maxLength(128)]
    },
    {
      name: 'code',
      label: 'Код *',
      initialValue: this.route.snapshot.data[0].code,
      validators: [Validators.required, Validators.maxLength(30)]
    },
  ]

  constructor(
    private subjectsService: SubjectsService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.data[0].id;  

  }

  onSubmit(subjectsForm: FormGroup) {
    if (subjectsForm.invalid) {
      this.errorMessage = 'Всички полета са задължителни';
      return;
    }

    const name = subjectsForm.value['name'];
    const code = subjectsForm.value['code'];

    this.subjectsService.updateSubject(this.id, name, code).subscribe({
      next: () => {
        this.toastr.success('Предметът беше променен успешно');
        this.router.navigate(['/', 'subjects', 'all']);
      },
      error: errorMessage => {
        this.errorMessage = errorMessage;
      }
    });
  }

}
