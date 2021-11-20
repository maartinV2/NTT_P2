import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageModel } from 'src/app/models/imageModel';
import { UserModel } from 'src/app/models/userModel';
import { FileService } from 'src/app/services/file.service';
import { ImageService } from 'src/app/services/image.service';
import { FormsModule } from '@angular/forms'
import { ReactiveFormsModule} from '@angular/forms'

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
                private fileService :FileService) { }



    LogedInUser:UserModel;
   currentDate : string = new Date().toDateString();
   maxDate: Date;
   progress= 0;
   image: ImageModel;

   newImage = new ImageModel;

   createForm: FormGroup;
   get uploadDateControl() {
    return this.createForm.controls.uploadDate;
  }

  ngOnInit(): void {
    this.maxDate = new Date;
    this.createForm = this.formBuilder.group({
      name: [''],
      type: [''],
      location: [''],
      uploadDate: [''],
    });
  }

  createPost$() {
   this.newImage.name=this.createForm.value.name;
   this.newImage.location=this.createForm.value.location;
   this.newImage.type=this.createForm.value.type;
    console.log(this.newImage);

    return this.imageService.createPost$(this.newImage);

  }


createPostClick(): void {
  this.createPost$().subscribe(insertedId => {
    console.log(insertedId);

    this.router.navigate(['profile', insertedId]);
  });
}

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }
    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    this.router.navigateByUrl('/animal', { skipLocationChange: true }).then(() => {
      this.fileService.uploadFile(formData, this.image.id);
      this.router.navigate(['/PostDetails/'+this.image.id]);
     });
   }
}



