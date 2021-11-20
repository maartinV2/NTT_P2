import { AgeModel } from "../models/age.model";
import { ImageModel as ImageModel } from "../models/imageModel";
import { UserModel } from "../models/userModel";
import { UserDto } from "./Userdto";

export class ImageDto  {
  id: string;
  name: string;
  age : AgeModel;
  uploadDateString: string;
  type: boolean;
  location:string;
  user: UserDto;
  url: string;

    ToModel(): ImageModel{
        const image = new ImageModel(
        );
        image.id = this.id;
        image.age=this.age;
        image.name=this.name;
        image.uploadDate = this.uploadDateString ? new Date(parseInt(this.uploadDateString.substr(0,4)),parseInt(this.uploadDateString.substr(4,2))-1,parseInt(this.uploadDateString.substr(6,8))) : null;
        image.type = this.type;
        image.location=this.location;
        image.user=  this.user ? Object.assign(new UserDto(), this.user).ToModel() : null;
        image.url= this.url;
        return image;
    }

    FromModel( image :ImageModel): ImageDto{
      const imageDto = new ImageDto(
      );
      imageDto.id = image.id;
      imageDto.age=image.age;
      imageDto.name=image.name;
      imageDto.uploadDateString = image.uploadDate?  [(image.uploadDate.getFullYear()).toString(), ((image.uploadDate.getMonth()+1)>9 ? '' : '0'),(image.uploadDate.getMonth()+1).toString(), ((image.uploadDate.getDate())>9 ? '' : '0') ,(image.uploadDate.getDate()).toString()].join(''): null;
      imageDto.type = image.type;
      imageDto.location=image.location;
      imageDto.user=  new UserDto().FromModel(image.user);
      imageDto.url= image.url;
      return imageDto;
  }


}
