import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { AuthenticateRequest } from '../models/request/authenticate-request.model';
import { RegisterRequest } from '../models/request/register-request.model';
import { AuthenticateResponse } from '../models/response/authenticate-response.model';
import { RegisterResponse } from '../models/response/register-response.model';
import { UserResponse } from '../models/response/user-response.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private intervalRef: any = null;
  public userLoggedIn = new Subject<void>();
  public pendingUsersChanged = new Subject<UserResponse[]>();

  constructor(private http: HttpClient) { }

  public login(usernameOrEmail: string, password: string) {
    const authenticateModel = new AuthenticateRequest(usernameOrEmail, password);

    return this.http.post<AuthenticateResponse>('/api/authenticate', authenticateModel)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          let errorMessage = 'Нещо се обърка!';
          if (error.status !== 400) {
            return throwError(errorMessage);
          }
          errorMessage = 'Въведени са невалидни данни!';
          return throwError(errorMessage);
        }),
        tap(response => {
          sessionStorage.setItem('token', response.jwtToken);
          this.autoRefreshToken(response.jwtToken);
          this.userLoggedIn.next();
        })
      );
  }


  public register(registerRequest: RegisterRequest) {
    return this.http.post<RegisterResponse>('/api/register', registerRequest)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          let errorMessage = 'Нещо се обърка!';
          if (error.error && error.error.User) {
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

  public getCurrentUserRole() {
    const token = this.getCurrentUserToken();
    if (!token) {
      return token;
    }
    const tokenDecrypted = JSON.parse(atob(token.split('.')[1]));
    return tokenDecrypted.role;
  }

  public refreshToken() {
    this.http.post<AuthenticateResponse>('/api/refresh-token', {}).subscribe({
      next: response => {
        sessionStorage.setItem('token', response.jwtToken);
        this.autoRefreshToken(response.jwtToken);
      },
      error: _ => {
        this.logout();
      }
    })
  }

  public approveUser(id: string) {
    return this.http.put<UserResponse>(`/api/approve/${id}`, {});
  }

  public rejectUser(id: string) {
    return this.http.put<UserResponse>(`/api/reject/${id}`, {});
  }

  public getPendingUsers() {
    this.http.get<UserResponse[]>('/api/users/pending').subscribe(users => {
      this.pendingUsersChanged.next(users);
    });
  }

  private autoRefreshToken(jwtToken: string) {
    const token = JSON.parse(atob(jwtToken.split('.')[1]));
    const expiresOnDate = new Date(token.exp * 1000);

    this.intervalRef = setTimeout(this.refreshToken.bind(this), expiresOnDate.getTime() - new Date().getTime() - 10000);
  }
}