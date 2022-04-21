import { Component, OnDestroy, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { UserResponse } from 'src/app/models/response/user-response.model';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-user-approve',
  templateUrl: './user-approve.component.html',
  styleUrls: ['./user-approve.component.css']
})
export class UserApproveComponent implements OnInit, OnDestroy {
  public pendingUsers: UserResponse[] = [];
  private pendingUsersSub!: Subscription;

  constructor(private usersService: UsersService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.pendingUsersSub = this.usersService.pendingUsersChanged.subscribe(users => {
      this.pendingUsers = users;    
      console.log(this.pendingUsers);
        
    });
    this.usersService.getPendingUsers();
  }

  onApprove(id: string) {
    this.usersService.approveUser(id).subscribe({
      next: _ => {
        this.toastr.success('Потребителят беше одобрен успешно');
        this.usersService.getPendingUsers();
      }
    })
  }

  onReject(id: string) {

  }

  ngOnDestroy(): void {
    this.pendingUsersSub.unsubscribe();
  }
}
