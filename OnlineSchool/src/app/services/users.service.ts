import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthenticateRequest } from '../models/request/authenticate-request.model';
import { AuthenticateResponse } from '../models/response/authenticate-response.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  public userLoggedIn = new Subject<void>();
  public loginError = new Subject<void>();

  constructor(private http: HttpClient) { }

  public login(usernameOrEmail: string, password: string) {
    const authenticateModel = new AuthenticateRequest(usernameOrEmail, password);

    this.http.post<AuthenticateResponse>('/api/authenticate', authenticateModel)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          this.loginError.next();
          return throwError(error);
        })
      )
      .subscribe(response => {
        sessionStorage.setItem('token', response.jwtToken);
        this.userLoggedIn.next();
      }
    );
  }
}
