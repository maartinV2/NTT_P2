import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {map} from "rxjs/operators"

import { HttpService } from './http/http.service';
import { UserModel } from '../models/userModel';
import { UserDto } from '../dto/Userdto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpService: HttpService) { }
  loggedInUser : UserModel;
  routeString = `${environment.apiUrl}/Users`;


  get$ = () =>{
    return this.httpService.get(this.routeString)
        .pipe(map(dto => {
          return (dto as UserDto[]).map(userDto => Object.assign(new UserDto(), userDto).ToModel());
      }));


  }


  getById$ = (userId : string):Observable<UserModel> => {
    let route : string = this.routeString+"/"+userId;
    return this.httpService.get(route)
    .pipe(map(model => {
        return (Object.assign(new UserDto(), model).ToModel());
    }))
  }
}
