import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ImageDto } from '../dto/imagedto';
import { ImageModel } from '../models/imageModel';
import { UserModel } from '../models/userModel';
import { HttpService } from './http/http.service';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  constructor(private httpService: HttpService,private router: Router) { }

  selectedUser : UserModel;
  routeString = `${environment.apiUrl}/Images`;


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


  createPost$ = (image: ImageModel) => {
    let route= `${this.routeString}`;
    let dto = new ImageDto;
    dto.FromModel(image);
    return this.httpService.post(route,  dto.FromModel(image))
}
}
