import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
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


  }

  title = 'minstagram';



  logout() {
    this.authService.logout();
    return false;
  }

  goToPage(): void {

    window.location.href = `${environment.identityProvider.stsAuthority}/Identity/Account/Manage/PersonalData`;
}

}
