import { HttpClient, HttpEventType } from "@angular/common/http";
import { EventEmitter, Injectable, Output } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn: 'root'
  })

  export class FileService {

    public progress: number;
    public message: string;
    imgUrl :string;


    constructor(private httpService: HttpClient) { }

    routeString = `${environment.apiUrl}/images/`;



    @Output() public onUploadFinished = new EventEmitter();

    public uploadFile(formData : FormData, image_id : string){
        const url = `${environment.apiUrl}/image/${image_id}`;

        this.httpService.post(url, formData, {reportProgress: true, observe: 'events'})
          .subscribe(event => {
            if (event.type === HttpEventType.UploadProgress)
            {
              this.progress = Math.round(100 * event.loaded / event.total);
            }
            else if (event.type === HttpEventType.Response) {
              this.message = 'Upload success.';
              this.onUploadFinished.emit(event.body);
            }
          });
        }
}








