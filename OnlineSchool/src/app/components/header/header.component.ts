import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {
  public loggedIn = false;
  public role!: string;
  private loggedInSub!: Subscription;

  constructor(private usersService: UsersService, private router: Router) { }

  ngOnInit(): void {
    this.loggedInSub = this.usersService.userLoggedIn.subscribe(() => {
      this.loggedIn = true;
      this.role = this.usersService.getCurrentUserRole();
    })
    const token = this.usersService.getCurrentUserToken();
    this.loggedIn = token ? true : false;
  }

  onLogout() {
    this.usersService.logout();
    this.loggedIn = false;
    this.router.navigate(['/', 'login']);
  }

  ngOnDestroy(): void {
    this.loggedInSub.unsubscribe();
  }

}
