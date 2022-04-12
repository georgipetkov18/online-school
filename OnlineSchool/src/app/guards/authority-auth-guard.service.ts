import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { UsersService } from '../services/users.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorityAuthGuardService implements CanActivate {

  constructor(
    private usersService: UsersService,
    private router: Router,
    private toastr: ToastrService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    const token = this.usersService.getCurrentUserToken();
    if (!token) {
      this.toastr.error('Необходимо е първо да влезете в профила си');
      return this.router.createUrlTree(['/login']);
    }

    const tokenDecrypted = JSON.parse(atob(token.split('.')[1]));
    if (tokenDecrypted.role !== 'administrator' && tokenDecrypted.role !== 'teacher') {
      this.toastr.error('Нямате права да достъпите тази страница');
      return this.router.createUrlTree(['/']);
    }
    return true;
  }
}
