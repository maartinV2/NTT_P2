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

    routeString = `${environment.apiUrl}/post`;



    @Output() public onUploadFinished = new EventEmitter();

    public UploadFile(formData : FormData, image_id : string, userId : string){
        const url = `${this.routeString}/image/${image_id}:${userId}`;

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








