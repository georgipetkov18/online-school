import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { UsersService } from '../../services/users.service';
import { RegisterRequest } from '../../models/request/register-request.model';
import { ClassesService } from '../../services/classes.service';
import { SubjectsService } from '../../services/subjects.service';
import { ClassResponse } from 'src/app/models/response/class-response.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @ViewChild('registerForm') registerForm!: NgForm;
  public errorMessage!: string;
  public suggestions!: string[];
  public role: 'student' | 'teacher' = 'student';

  constructor(
    private usersService: UsersService,
    private toastr: ToastrService,
    private router: Router,
    private classesService: ClassesService,
    private subjectsService: SubjectsService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    if (this.registerForm.invalid) {
      this.errorMessage = 'Всички полета са задължителни';
      return;
    }
    const password = this.registerForm.value['password'];
    const rePass = this.registerForm.value['rePassword'];
    if (password !== rePass) {
      this.errorMessage = 'Паролите не съвпадат';
      return;
    }
    const username = this.registerForm.value['username'];
    const firstName = this.registerForm.value['firstName'];
    const lastName = this.registerForm.value['lastName'];
    const email = this.registerForm.value['email'];
    const userSpecific = this.registerForm.value['userSpecific'];
    const registerRequest = new RegisterRequest(username, password, firstName, lastName, email, this.role);
    this.role === 'teacher' ? registerRequest.subjectId = userSpecific : registerRequest.classId = userSpecific;

    this.usersService.register(registerRequest)
      .subscribe({
        next: response => {
          this.toastr.success('Регистрацията беше успешна');
        },
        error: errorMessage => {
          this.errorMessage = errorMessage;
        }
      }
      );
  }

  onGetOptions(input: HTMLInputElement, menu: HTMLDivElement) {
    if (input.value === '') {
      this.suggestions = [];
      menu.classList.remove('show');
      return;
    }
    else {
      menu.classList.add('show');
      switch (this.role) {
        case 'student':
          // TODO: Get classes with given input
          this.classesService.getAllClasses().subscribe({
            next: classes => {
              this.suggestions = classes
                .filter(s => s.name.includes(input.value))
                .map(c => c.name);
            }
          })
          break;

        case 'teacher':
          // TODO: Get subjects with given input
          this.subjectsService.getAllSubjects().subscribe({
            next: subjects => {
              this.suggestions = subjects
                .filter(s => s.name.includes(input.value))
                .map(s => s.name);
            }
          })
          break;
      }
    }
  }

  onSelect(role: 'student' | 'teacher') {
    this.role = role;
  }

  onFormReset() {
    this.registerForm.reset();
  }
}
