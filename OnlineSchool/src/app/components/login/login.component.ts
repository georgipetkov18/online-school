import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { UtilityService } from '../../services/utility.service';
import { UsersService } from '../../services/users.service';
import { IAppFormControl } from '../shared/form/form.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public errorMessage!: string;
  public formSetup: IAppFormControl[] = [
    {
     name: 'usernameOrEmail',
     label: 'Потрбителско име или имейл *',
     validators: [Validators.required]
    },
    {
      name: 'password',
      label: 'Парола *',
      inputType: 'password',
      validators: [Validators.required]
    }
  ]
  constructor(
    public utilityService: UtilityService,
    private usersService: UsersService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(loginForm: FormGroup) {
    if (loginForm.invalid) {
      this.errorMessage = 'Всички полета са задължителни';
      return;
    }
    const usernameOrEmail = loginForm.value['usernameOrEmail'];
    const password = loginForm.value['password'];

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
