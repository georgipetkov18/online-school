import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsersService } from './services/users.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private usersService: UsersService, private router: Router) { }

  ngOnInit(): void {
    const token = this.usersService.getCurrentUserToken();
    token ? this.router.navigate(['timetable/info']): this.router.navigate(['/login']);
  }

  title = 'OnlineSchool';
}
