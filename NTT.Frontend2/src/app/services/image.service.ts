import { HttpHeaders } from '@angular/common/http';
import { Injectable, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ImageDto } from '../dto/imagedto';
import { ImageModel } from '../models/imageModel';
import { UserModel } from '../models/userModel';
import { FileService } from './file.service';
import { HttpService } from './http/http.service';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private httpService: HttpService,private router: Router,private fileService:FileService) { }

  selectedUser : UserModel;
  routeString = `${environment.apiUrl}/Images`;
  insertedId:string;

  selectedImage : ImageModel;
  get$ = () => {
  let route : string = this.routeString;
    return this.httpService.get(route)
    .pipe(map(dto => {
      return (dto as ImageDto[]).map(d => Object.assign(new ImageDto(), d).ToModel());
    }))
  }
  setSelectedImage = (image: ImageModel) => {
    this.selectedImage = image;
  }

  getById = (imageId : string):Observable<ImageModel> => {
    let route : string = this.routeString+"/"+imageId;
    return this.httpService.get(route)
    .pipe(map(model => {
        return (Object.assign(new ImageDto(), model).ToModel());
    }))
  }


  createPost$ = (image: ImageModel, formData: FormData) => {
    let route= `${this.routeString}`;
    let dto = new ImageDto;
    dto.FromModel(image);
     return this.httpService.post(route,  dto.FromModel(image)).subscribe(insertedId=>{
      this.fileService.UploadFile(formData, insertedId.toString(),image.user.id.toString())

      this.router.navigate(['PostDetails', insertedId]);
      return insertedId;
    })

  }

  updatePost$ = (updatedImage: ImageModel) => {
    let route= `${this.routeString}`;
    let dto = new ImageDto;
    dto.FromModel(updatedImage);
     return this.httpService.put(route,  dto.FromModel(updatedImage)).subscribe(insertedId=>{
      this.router.navigate(['PostDetails', insertedId]);
      return insertedId;
    })

}
}





