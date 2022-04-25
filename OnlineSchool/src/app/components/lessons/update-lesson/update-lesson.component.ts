import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LessonsService } from 'src/app/services/lessons.service';
import { IAppFormControl } from '../../shared/form/form.component';

@Component({
  selector: 'app-update-lesson',
  templateUrl: './update-lesson.component.html',
  styleUrls: ['./update-lesson.component.css']
})
export class UpdateLessonComponent implements OnInit {
  public errorMessage!: string;
  private id!: string;
  public formSetup: IAppFormControl[] = [
    {
      name: 'from',
      label: 'От *',
      inputType: 'time',
      initialValue: this.route.snapshot.data[0].from,
      validators: [Validators.required]
    },
    {
      name: 'duration',
      label: 'Минути *',
      inputType: 'number',
      initialValue: this.route.snapshot.data[0].durationInMinutes,
      validators: [Validators.required, Validators.min(5), Validators.max(100)]
    }
  ]

  constructor(
    private lessonsService: LessonsService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router) {}

  ngOnInit(): void {  
    this.id = this.route.snapshot.data[0].id;  
  }

  onSubmit(lessonsForm: FormGroup) {
    if (lessonsForm.invalid) {
      this.errorMessage = 'Всички полета са задължителни';
      return;
    }

    const from = lessonsForm.value['from'] + ':00';
    const durationInMinutes = +lessonsForm.value['duration'];

    this.lessonsService.updateLesson(this.id, from, durationInMinutes).subscribe({
      next: () => {
        this.toastr.success('Часът беше променен успешно');
        this.router.navigate(['/', 'lessons', 'all']);
      },
      error: errorMessage => {
        this.errorMessage = errorMessage;
      }
    });
  }

}
