import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageModel } from 'src/app/models/imageModel';
import { UserModel } from 'src/app/models/userModel';
import { FileService } from 'src/app/services/file.service';
import { ImageService } from 'src/app/services/image.service';
import { FormsModule } from '@angular/forms'
import { ReactiveFormsModule} from '@angular/forms'
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  constructor(  private route: ActivatedRoute,
                private router: Router,
                private formBuilder: FormBuilder,
                private imageService :ImageService,
                private fileService :FileService,
                private authService :AuthService,
                private userService:UserService) { }



   currentDate :string = new Date().toDateString();;
   maxDate: Date;
   progress= 0;
   image: ImageModel;
   newImage = new ImageModel;

   LogedInUserId:string;
   LoggedInUser:UserModel;

   InsertedId:string;
   formData : FormData;
   createForm: FormGroup;


  ngOnInit(): void {
    this.maxDate = new Date;
    this.createForm = this.formBuilder.group({
      name: [''],
      type: [''],
      location: [''],
      uploadDate: [''],
    });

    this.getUserId();
  }



  getUserId(): void {
    this.authService.getUser()
   .subscribe(data => {
     this.LogedInUserId=data.profile.sub;
     this.getLoggedInUser( this.LogedInUserId);

   });
  }

  getLoggedInUser( userId: string):void{
   this.userService.getById$(userId).subscribe
   (user=>{
     console.log(user);

     this.LoggedInUser=user;
   })
  }


  createPostClick(): void {
    this.createPost();
    };




     public uploadFile = (file) => {
      if (file.length === 0) {
        return;
      }
      let fileToUpload = <File>file[0];
      this.formData = new FormData();
      this.formData.append('noteFile', fileToUpload, fileToUpload.name);
     }

     createPost() {
      this.newImage.name=this.createForm.value.name;
      this.newImage.location=this.createForm.value.location;
      this.newImage.uploadDate= new Date();



      if(this.createForm.value.type=="Public"){
       this.newImage.type=true;
      }
      else{
       this.newImage.type=false;
      }
      this.newImage.user=this.LoggedInUser;

      this.imageService.createPost$(this.newImage,this.formData);


     }


  }


