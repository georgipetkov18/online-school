import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { UtilityService } from '../../services/utility.service';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  @ViewChild('loginForm') loginForm!: NgForm;
  public errorMessage!: string;

  constructor(
    public utilityService: UtilityService,
    private usersService: UsersService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      this.errorMessage = 'Всички полета са задължителни';
      return;
    }
    const usernameOrEmail = this.loginForm.value['usernameOrEmail'];
    const password = this.loginForm.value['password'];

    this.usersService.login(usernameOrEmail, password)
      .subscribe({
        next: response => {
          this.toastr.success('Успешно влизане в системата');
          this.router.navigate(['/']);
        },
        error: errorMessage => {
          this.errorMessage = errorMessage;
        }
      }
    );
  }
}
