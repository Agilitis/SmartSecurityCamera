import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-yolo',
  templateUrl: './yolo.component.html',
})
export class YoloComponent {
  fileToUpload: File = null;
  public message: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);

    this.postFile(this.fileToUpload).subscribe(result => {
      this.message = result.message;
    });
  }

  postFile(fileToUpload: File): Observable<any> {
    let endpoint: string;
    if (!!this.baseUrl) {
      endpoint = 'https://localhost:5001/api/Yolo';
    } else {
      endpoint = "https://objectdetectionwithtraining-dev-as.azurewebsites.net/api/Yolo";
    }
    const formData: FormData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    return this.http.post<any>(endpoint, formData, { reportProgress: true });
  }
}
