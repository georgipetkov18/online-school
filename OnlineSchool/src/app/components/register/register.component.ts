import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { UsersService } from '../../services/users.service';
import { RegisterRequest } from '../../models/request/register-request.model';
import { ClassesService } from '../../services/classes.service';
import { SubjectsService } from '../../services/subjects.service';

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
    const value = input.value;
    if (value === '') {
      this.suggestions = [];
      menu.classList.toggle('show');
      return;
    }
    else {
      menu.classList.add('show');
      switch (this.role) {
        case 'student':
          this.classesService.getAllClasses(value).subscribe({
            next: classes => {
              this.suggestions = classes.map(c => c.name);
            }
          })
          break;

        case 'teacher':
          if (value.length < 3) {
            this.suggestions = [];
            menu.classList.toggle('show');
            return;
          }
          this.subjectsService.getAllSubjects(value).subscribe({
            next: subjects => {
              this.suggestions = subjects.map(s => s.name);
            }
          })
          break;
      }
    }
  }

  onChooseSuggestion(suggestion: string, input: HTMLInputElement, menu: HTMLDivElement) {
    input.value = suggestion;
    menu.classList.toggle('show');
  }

  onSelect(role: 'student' | 'teacher') {
    this.role = role;
  }

  onFormReset() {
    this.registerForm.reset();
  }
}
