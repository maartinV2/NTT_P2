import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {map} from "rxjs/operators"

import { HttpService } from './http/http.service';
import { UserModel } from '../models/userModel';
import { UserDto } from '../dto/Userdto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpService: HttpService) { }
  selecteduser : UserModel;
  routeString = `${environment.apiUrl}/Users`;


  get$ = () =>{
    return this.httpService.get(this.routeString)
        .pipe(map(dto => {
          return (dto as UserDto[]).map(userDto => Object.assign(new UserDto(), userDto).ToModel());
      }));

  }
}
