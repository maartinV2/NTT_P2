import { Component, OnInit } from '@angular/core';
import { UserModel } from 'src/app/models/userModel';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public users :UserModel[];

  constructor(private userService : UserService) { }

  ngOnInit(): void {
    this.getAllUsers();
      console.log(this.users);

  }

  getAllUsers(){
    this.userService.getUsers$().subscribe(users => {
      this.users = users as UserModel[];
   });
}
}
