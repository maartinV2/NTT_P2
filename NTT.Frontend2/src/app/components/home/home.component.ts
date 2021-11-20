import { Component, OnInit } from '@angular/core';
import { UserModel } from 'src/app/models/userModel';
import { AuthService } from 'src/app/services/auth.service'

import { UserService } from 'src/app/services/user.service';

import { LoaderService } from 'src/app/services/loader.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { ImageService } from 'src/app/services/image.service';
import { ImageModel } from 'src/app/models/imageModel';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {



  constructor(private userService : UserService,
             private loaderService: LoaderService,
             private imageService:ImageService,
             private router: Router) { }
  users ;
  images: ImageModel[];
  destroy$ =new Subject<void>();
  isLoading = true;

  allUsers : UserModel[];

  ngOnInit(): void {

    this.loaderService.isLoading$.pipe(takeUntil(this.destroy$)).subscribe(isLoading => {
      this.isLoading = isLoading
    });

    this.imageService.get$().pipe(takeUntil(this.destroy$)).subscribe(images =>{
      this.images=images;


    });
  }


  PostClicked(image:ImageModel){
    this.imageService.setSelectedImage(image);
    this.router.navigate(['PostDetails',  image.id]);

  }




}




