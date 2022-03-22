import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SubjectsService } from '../../../services/subjects.service';
import { UtilityService } from '../../../services/utility.service';

@Component({
  selector: 'app-create-subject',
  templateUrl: './create-subject.component.html',
  styleUrls: ['./create-subject.component.css']
})
export class CreateSubjectComponent implements OnInit {
  @ViewChild('createSubjectForm') public subjectsForm!: NgForm;
  public errorMessage!: string;

  constructor(
    public utilityService: UtilityService,
    private subjectsService: SubjectsService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    if (this.subjectsForm.invalid) {
      this.errorMessage = 'Всички полета са задължителни';
      return;
    }

    const name = this.subjectsForm.value['name'];
    const code = this.subjectsForm.value['code'];

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
