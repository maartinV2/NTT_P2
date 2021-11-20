import { UserModel } from "../models/userModel";


export class UserDto {

  id : number;
  name: string;

    ToModel(): UserModel{
        var user = new UserModel();
        user.id = this.id;
        user.name=this.name

        return user;

    }

    FromModel(User : UserModel): UserDto{
        const Userdto = new UserDto();
            Userdto.id= User.id;
            Userdto.name = User.name;

        return Userdto;
    }


}
