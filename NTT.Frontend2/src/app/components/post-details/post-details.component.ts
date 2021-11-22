import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageModel } from 'src/app/models/imageModel';
import { UserModel } from 'src/app/models/userModel';
import { ImageService } from 'src/app/services/image.service';

@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrls: ['./post-details.component.css']
})
export class PostDetailsComponent implements OnInit {

  image :ImageModel;
  user :UserModel;
  imageId : string;
  imageDetailsForm: any;
  constructor(private router: Router,
    private route: ActivatedRoute,
    private imageService: ImageService,
    public formBuilder: FormBuilder,

   ) { }

  ngOnInit(): void {

    this.route.paramMap.subscribe(params => {
      this.imageId = params.get("id");
    });

    this.imageService.getById(this.imageId).subscribe(image => {
      this.image = image;
      console.log(this.image );

      setTimeout;
     });


    this.imageDetailsForm = this.formBuilder.group({
      image_location: [''],
      image_name: [''],
      image_public: ['']
    })
  }


  clickbackHome(){
    this.router.navigate(['/home'])
  }

  eidtPostClick(){
console.log(this.image);

    this.imageService.updatePost$(this.image);
  }

  status: boolean = false;
  editToggleEvent(){
      this.status = !this.status;
  }


}
