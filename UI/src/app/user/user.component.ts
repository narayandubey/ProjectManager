import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { User } from '../models/user';
import { EventService } from '../services/event.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  users: Array<User>;
  userToAdd: User;
  buttonName: string;
  isFirstNameAsc: any;
  isLastNameAsc: any;
  isEmpIdAsc: any;

  constructor(private userService: UserService, private eventService: EventService) {
    this.users = new Array<User>();

  }

  ngOnInit() {
    this.userToAdd = new User();
    this.buttonName = 'Add';
    this.eventService.showLoading(true);
    this.userService.getUser().subscribe((users) => {
      this.users = users;
      this.eventService.showLoading(false);
    },
      (error) => {
        this.eventService.showError(error);
        this.eventService.showLoading(false);
      });
  }

  addUser() {
    if (!this.userToAdd.FirstName || this.userToAdd.FirstName === '') {
      this.eventService.showWarning('Please add first Name ');
      return;
    }
    if (!this.userToAdd.LastName || this.userToAdd.LastName === '') {
      this.eventService.showWarning('Please add last Name ');
      return;
    }
    if (!this.userToAdd.EmployeeId || this.userToAdd.EmployeeId === '') {
      this.eventService.showWarning('Please enter employee id');
      return;
    }
    if (this.buttonName === 'Add') {
      this.eventService.showLoading(true);
      this.userService.addUser(this.userToAdd).subscribe((data) => {
        this.eventService.showSuccess('Saved successfully')
        this.ngOnInit();
        this.eventService.showLoading(false);
      },
        (error) => {
          this.eventService.showError(error);
          this.eventService.showLoading(false);
        });
    }
    if (this.buttonName === 'Update') {
      this.eventService.showLoading(true);
      this.userService.updateUser(this.userToAdd).subscribe((data) => {
        this.eventService.showSuccess('Update successfully')
        this.ngOnInit();
        this.eventService.showLoading(false);
      },
        (error) => {
          this.eventService.showError(error);
          this.eventService.showLoading(false);
        });
    }
  }

  resetUser() {
    this.userToAdd = new User();
    this.buttonName = 'Add';
  }

  editUser(user) {
    this.buttonName = 'Update';
    this.userToAdd = user;
  }

  deleteUser(user) {
    this.eventService.showLoading(true);
    this.userService.deleteUser(user).subscribe((data) => {
      this.eventService.showSuccess('User Deleted successfully')
      this.ngOnInit();
      this.eventService.showLoading(false);
    },
      (error) => {
        this.eventService.showError(error);
        this.eventService.showLoading(false);
      });
  }

  sortUser(type: number) {
    if (type === 1) {
      this.isFirstNameAsc = !this.isFirstNameAsc;
      const direction = this.isFirstNameAsc ? 1 : -1;
      this.users.sort((a, b) => (a.FirstName > b.FirstName) ? 1 * direction
        : ((b.FirstName > a.FirstName) ? -1 * direction : 0));
    }
    if (type === 2) {
      this.isLastNameAsc = !this.isLastNameAsc;
      const direction = this.isLastNameAsc ? 1 : -1;
      this.users.sort((a, b) => (a.LastName > b.LastName) ? 1 * direction :
        ((b.LastName > a.LastName) ? -1 * direction : 0));
    }
    if (type === 3) {
      this.isEmpIdAsc = !this.isEmpIdAsc;
      const direction = this.isEmpIdAsc ? 1 : -1;
      this.users.sort((a, b) => (a.EmployeeId > b.EmployeeId) ? 1 * direction :
        ((b.EmployeeId > a.EmployeeId) ? -1 * direction : 0));
    }
    this.users = [...this.users];
  }
}
