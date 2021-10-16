import { Injectable } from "@angular/core";
import { UserModel } from "../models/user.model";
import {map} from "rxjs/operators"
import { HttpService } from  "./http/http.service"
import { environment } from "src/environments/environment";
import { UserDto } from "../dto/userDto"
import { BehaviorSubject, Observable } from 'rxjs';


@Injectable({
    providedIn: 'root'
})

export class UserService{


  public Users:UserModel[];

    constructor(private httpService : HttpService){}
    routeString = `${environment.apiUrl}/User`;

    public selectedUsers$ = new BehaviorSubject<UserModel[]>([]);
    public selectedUser$ = new BehaviorSubject<UserModel>(null);
    selectedUser : UserModel;
    selectedFilteredFarmer: number;

    getAllFromApi$ = () => {
      let route : string = this.routeString;
      return this.httpService.get(route)
          .pipe(map(dto => {
            return (dto as UserDto[]).map(d => Object.assign(new UserDto(), d).ToModel());
        }));
    }

    getByIdFromAPi$ = (id : number):Observable<UserModel> =>{
      let route : string = `${this.routeString}/${id}`;
      return this.httpService.get(route)
      .pipe(map(dto => {
        return (Object.assign(new UserDto(), dto).ToModel());
      }));
    }

    setSelectedUser = (User: UserModel) => {
      this.selectedUser = User;
    }

    create$ = (User: UserModel) => {
      let route= `${this.routeString}`;
      let dto = new UserDto;
      dto.FromModel(User);
      return this.httpService.post(route,  dto.FromModel(User))
  }

  update$ = (UserToUpdate: UserModel, UserId: number) => {
    let route= `${this.routeString}/${UserId}`;
    let dto = new UserDto;
    console.log( dto.FromModel(UserToUpdate));

    return this.httpService.put(route, dto.FromModel(UserToUpdate));
  }

}
