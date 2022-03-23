import { Component, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { UsersService } from '../../services/users.service';
import { RegisterRequest } from '../../models/request/register-request.model';
import { ClassesService } from '../../services/classes.service';
import { SubjectsService } from '../../services/subjects.service';
import { UtilityService } from '../../services/utility.service';
import { ClassResponse } from '../../models/response/class-response.model';
import { SubjectResponse } from 'src/app/models/response/subject-response.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @ViewChild('registerForm') registerForm!: NgForm;
  public errorMessage!: string;
  public suggestions!: ClassResponse[] | SubjectResponse[];
  public role: 'student' | 'teacher' = 'student';
  public userSpecificInput!: string;

  constructor(
    public utilityService: UtilityService,
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
    const filtered = this.suggestions.filter(s => s.name.toLowerCase() === userSpecific.toLowerCase());
    
    if (filtered.length <= 0) {
      this.errorMessage = this.role === 'teacher' ? 'Предметът не същестува' : 'Класът не съществува';
      return;
    }
    
    const item = filtered[0];
    const registerRequest = new RegisterRequest(username, password, firstName, lastName, email, this.role);
    this.role === 'teacher' ? registerRequest.subjectId = item.id : registerRequest.classId = item.id;

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

  onGetOptions(event: string, menu: HTMLDivElement) {
    if (event === '') {
      this.suggestions = [];
      menu.classList.toggle('show');
      return;
    }
    else {
      menu.classList.add('show');
      switch (this.role) {
        case 'student':
          this.classesService.getAllClasses(event).subscribe({
            next: classes => {
              this.suggestions = classes;
            }
          })
          break;

        case 'teacher':
          if (event.length < 3) {
            this.suggestions = [];
            menu.classList.toggle('show');
            return;
          }
          this.subjectsService.getAllSubjects(event).subscribe({
            next: subjects => {
              this.suggestions = subjects;
            }
          })
          break;
      }
    }
  }

  onChooseSuggestion(suggestion: string, input: HTMLInputElement, menu: HTMLDivElement) {
    this.userSpecificInput = suggestion;
    menu.classList.toggle('show');
  }

  onSelect(role: 'student' | 'teacher') {
    this.role = role;
  }
}
