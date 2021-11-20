import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ImageModel } from 'src/app/models/imageModel';
import { UserModel } from 'src/app/models/userModel';
import { FileService } from 'src/app/services/file.service';

import { HttpClient, HttpEventType } from '@angular/common/http';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {


  user: UserModel;
  image: ImageModel;
  imgUrl :string;



  userForm: any;
  formData : FormData;

  public progress: number;
  public message: string;


  constructor(private router: Router,
    private httpService : HttpClient,
    private fileservice : FileService,
  ) { }

  ngOnInit(): void {



  }

  public uploadFile = (noteFiles) => {
    if (noteFiles.length === 0) {
      return;
    }
    let fileToUpload = <File>noteFiles[0];
    this.formData = new FormData();
    this.formData.append('noteFile', fileToUpload, fileToUpload.name);
   }

}
