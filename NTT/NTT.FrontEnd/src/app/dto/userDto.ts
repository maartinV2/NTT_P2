import { UserModel } from "../models/user.model";

export class UserDto {

  Id : number;
  Name :string;
  Surname :string;
  Phone :string;
  Email :string;


    ToModel(): UserModel{
        const User = new UserModel(
        );
        User.Id = this.Id;
        User.Name = this.Name;
        User.Surname = this.Surname;
        User.Email = this.Email;
        User.Phone = this.Phone;
        return User;
    }

    FromModel(User : UserModel): UserDto{
        const Userdto = new UserDto(
            );
            Userdto.Name = User.Name;
            Userdto.Surname =User.Surname;
            Userdto.Email =User.Email;
            Userdto.Phone =User.Phone;

        return Userdto;
    }


}
