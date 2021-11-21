import { Component, OnInit } from '@angular/core';
import { timeout } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserModel } from './models/userModel';
import { AuthService } from './services/auth.service';
import { UserService } from './services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit  {

  constructor(private userService: UserService,private authService: AuthService){}
  ngOnInit(): void {

  this.getUserId();

  }

  title = 'minstagram';

LogedInUserId:string;
LoggedInUser:UserModel;


  logout() {
    this.authService.logout();
    return false;
  }

  goToPage(): void {

    window.location.href = `${environment.identityProvider.stsAuthority}/Identity/Account/Manage/PersonalData`;
}

getUserId(): void {
  this.authService.getUser()
 .subscribe(data => {
   this.LogedInUserId=data.profile.sub;
   this.getLoggedInUser( this.LogedInUserId);

 });
}

getLoggedInUser( userId: string):void{
 this.userService.getById$(userId).subscribe
 (user=>{
   this.LoggedInUser=user;
 })
}

}
