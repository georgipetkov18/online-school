import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {
  @ViewChild('loginForm') loginForm!: NgForm;
  public loginSubscription!: Subscription;
  public loginErrorSubscription!: Subscription;
  public errorOccured = false;

  constructor(private usersService: UsersService, private router: Router) { }

  ngOnInit(): void {
    this.loginSubscription = this.usersService.userLoggedIn.subscribe(() => {
      this.router.navigate(['/']);
    });

    this.loginErrorSubscription = this.usersService.loginError.subscribe(() => {
      this.errorOccured = true;
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) {
      //TODO: Implement user experience
      return;
    }
    const usernameOrEmail = this.loginForm.value['usernameOrEmail'];
    const password = this.loginForm.value['password'];

    this.usersService.login(usernameOrEmail, password);
  }

  ngOnDestroy(): void {
    this.loginSubscription.unsubscribe();
    this.loginErrorSubscription.unsubscribe();
  }

}
