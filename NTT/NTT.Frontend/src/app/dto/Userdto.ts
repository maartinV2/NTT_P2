import { UserModel as UserModel } from "../models/userModel";

export class UserDto {

  Id : number;
  Name: string;

    ToModel(): UserModel{
        const user = new UserModel(
        );
        user.id = this.Id;
        user.name=this.Name

        return user;
    }

    FromModel(User : UserModel): UserDto{
        const Userdto = new UserDto(
            );
            Userdto.Id= User.id;
            Userdto.Name = User.name;

        return Userdto;
    }


}
