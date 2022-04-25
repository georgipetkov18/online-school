import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-home',
  template: '<div></div>',
})
export class HomeComponent implements OnInit {

  constructor(private usersService: UsersService, private router: Router) { }

  ngOnInit(): void {
    const userRole = this.usersService.getCurrentUserRole();
    let pathArray: string[] = [];

    if (!userRole) {
      pathArray = ['/', 'login'];
    }
    else if (userRole === 'administrator') {
      pathArray = ['/', 'admin']
    }  
    else {
      pathArray = ['/', 'timetable', 'info'];
    }
    this.router.navigate(pathArray);

  }

}
