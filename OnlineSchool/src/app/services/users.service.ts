import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { AuthenticateRequest } from '../models/request/authenticate-request.model';
import { RegisterRequest } from '../models/request/register-request.model';
import { AuthenticateResponse } from '../models/response/authenticate-response.model';
import { RegisterResponse } from '../models/response/register-response.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private intervalRef: any = null;

  constructor(private http: HttpClient) { }

  public login(usernameOrEmail: string, password: string) {
    const authenticateModel = new AuthenticateRequest(usernameOrEmail, password);

    return this.http.post<AuthenticateResponse>('/api/authenticate', authenticateModel)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          let errorMessage = 'Нещо се обърка!';
          if (error.status !== 400) {
            return throwError(() => errorMessage);
          }
          errorMessage = 'Въведени са невалидни данни!';
          return throwError(errorMessage);
        }),
        tap(response => {
          this.autoRefreshToken(response.jwtToken);
          sessionStorage.setItem('token', response.jwtToken);
        }
        )
      );
  }


  public register(registerRequest: RegisterRequest) {
    return this.http.post<RegisterResponse>('/api/register', registerRequest)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          let errorMessage = 'Нещо се обърка!';
          if (error.error && error.error.User) {
            console.log(error.error.User[0]);

            errorMessage = error.error.User[0];
          }

          return throwError(errorMessage);
        })
      )
  }

  public logout() {
    sessionStorage.clear();
    if (this.intervalRef) {
      clearTimeout(this.intervalRef);
    }
  }

  public getCurrentUserToken() {
    return sessionStorage.getItem('token');
  }

  private refreshToken() {
    this.http.post<AuthenticateResponse>('/api/refresh-token', {}, {
      headers: new HttpHeaders().append('Authorization', `Bearer ${sessionStorage.getItem('token')}`)
    }).subscribe({
      next: response => {
        this.autoRefreshToken(response.jwtToken);
        sessionStorage.setItem('token', response.jwtToken);
      },
      error: _ => {
        this.logout();
      }
    })
  }

  private autoRefreshToken(jwtToken: string) {
    const token = JSON.parse(atob(jwtToken.split('.')[1]));
    const expiresOnDate = new Date(token.exp * 1000);

    this.intervalRef = setTimeout(this.refreshToken.bind(this), expiresOnDate.getTime() - new Date().getTime() - 10000);
  }
}