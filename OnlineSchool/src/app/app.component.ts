import { Component, OnInit } from '@angular/core';
import { UsersService } from './services/users.service';
import { SignalRService } from './services/signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private usersService: UsersService, private signalRService: SignalRService) { }

  ngOnInit(): void {
    this.usersService.refreshToken();

    this.signalRService.hubHelloMessage.subscribe((hubHelloMessage: string | null) => {
      console.log(hubHelloMessage);
    });

    this.signalRService.connection.invoke('GetData')
  }

  title = 'OnlineSchool';
}
