import { AgeModel } from "./age.model";
import { UserModel } from "./userModel";
export class ImageModel {

     id: string;
     name: string;
     age : AgeModel;
     uploadDate: Date;
     type: boolean;
     location:string;
     user: UserModel;
     url: string;


}

