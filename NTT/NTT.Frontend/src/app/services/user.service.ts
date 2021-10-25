import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {map} from "rxjs/operators"

import { HttpService } from './http/http.service';
import { UserDto } from '../dto/Userdto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpService: HttpService) { }



  getUsers$ = () => {
    let route: string = `${environment.apiUrl}/Users`;
    return this.httpService.get(route)
        .pipe(map(dto => {
          return (dto as UserDto[]).map(d => Object.assign(new UserDto(), d).ToModel());
      }));

  }
}
