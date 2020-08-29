import { Component, Inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Component({
    selector: 'app-home',
    templateUrl: './azure-cognitive.component.html',
  })
  export class AzureCognitiveComponent {
      
    fileToUpload: File = null;
    public message: string;
  
    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }
  
    handleFileInput(files: FileList) {
      this.fileToUpload = files.item(0);
  
      this.postFile(this.fileToUpload).subscribe(result => {
        this.message = result.message;
        console.log(this.message);
      });
    }
  
    postFile(fileToUpload: File): Observable<any> {
      let endpoint: string;
      if (!!this.baseUrl) {
        endpoint = 'https://localhost:5001/api/AzureCognitiveServices';
      } else {
        endpoint = "https://objectdetectionwithtraining-dev-as.azurewebsites.net/api/AzureCognitiveServices";
      }
      const formData: FormData = new FormData();
      formData.append('file', fileToUpload, fileToUpload.name);
      return this.http.post<any>(endpoint, formData, { reportProgress: true });
    }
  }