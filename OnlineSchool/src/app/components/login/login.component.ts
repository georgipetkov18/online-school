import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

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
    private usersService: UsersService, 
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      //TODO: Implement user experience
      return;
    }
    const usernameOrEmail = this.loginForm.value['usernameOrEmail'];
    const password = this.loginForm.value['password'];

    this.usersService.login(usernameOrEmail, password)
      .subscribe(response => {
        this.router.navigate(['/']);
      }, errorMessage => {
        this.errorMessage = errorMessage;
      }
    );
  }
}
