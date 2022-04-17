import { Component, OnInit } from '@angular/core';
import { UsersService } from './services/users.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private usersService: UsersService) { }

  ngOnInit(): void {
    const token = this.usersService.getCurrentUserToken();
    if (token) {
      this.usersService.refreshToken();
    }
  }

  title = 'OnlineSchool';
}
