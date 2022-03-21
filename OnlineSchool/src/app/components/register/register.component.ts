import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { UsersService } from '../../services/users.service';
import { RegisterRequest } from '../../models/request/register-request.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @ViewChild('registerForm') registerForm!: NgForm;
  public errorMessage!: string;

  constructor(
    private usersService: UsersService,
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit() {
    if (this.registerForm.invalid) {
      this.errorMessage = 'Всички полета са задължителни';
      return;
    }
    const username = this.registerForm.value['username'];
    const password = this.registerForm.value['password'];
    const firstName = this.registerForm.value['firstName'];
    const lastName = this.registerForm.value['lastName'];
    const email = this.registerForm.value['email'];
    const roleName = 'student';
    const registerRequest = new RegisterRequest(username, password, firstName, lastName, email, roleName);

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

  onFormReset() {
    this.registerForm.reset();
  }
}
