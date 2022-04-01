import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public connection!: HubConnection;
  public hubHelloMessage = new BehaviorSubject<string | null>(null);

  constructor() {}

  public initiateConnection(): Promise<void>{
    return new Promise((resolve, reject) => {
      this.connection = new HubConnectionBuilder()
        .withUrl('/api/timetableHub')
        .build();
  
      this.setClientMethods();

      this.connection
        .start()
        .then(() => {
          console.log(`SignalR connection success! connectionId: ${this.connection.connectionId} `);
          resolve();
        })
        .catch((error) => {
          console.log(`SignalR connection error: ${error}`);
          reject();
        });
    });
  }

  private setClientMethods(): void {
    this.connection.on('SendInfo', (message: string) => {
      console.log('get data ', message);
      
      this.hubHelloMessage.next(message);
    });
  }
  
}
