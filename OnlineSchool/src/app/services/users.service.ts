import { HttpClient, HttpErrorResponse } from '@angular/common/http';
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
          sessionStorage.setItem('token', response.jwtToken);
        }
      )
    );
  }


  public register(registerRequest: RegisterRequest) {
    return this.http.post<RegisterResponse>('/api/register', registerRequest)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          console.log(error);
          
          let errorMessage = 'Нещо се обърка!';
          if (error.error && error.error.User) {
            console.log(error.error.User[0]);
            
            errorMessage = error.error.User[0];
          }

          return throwError(errorMessage);
        })
      )
  }
}