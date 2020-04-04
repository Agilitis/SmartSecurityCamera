import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-yolo',
  templateUrl: './sentiment.component.html',
})
export class SentimentComponent {
  fileToUpload: File = null;
  public message: string;
  public sentence: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);

    this.postSentence().subscribe(result => {
      this.message = result.message;
    });
  }

  postSentence(): Observable<any> {
    let endpoint: string;
    if (!!this.baseUrl) {
      endpoint = 'https://localhost:5001/api/Yolo';
    } else {
      endpoint = "https://objectdetectionwithtraining-dev-as.azurewebsites.net/api/Yolo";
    }
    
    return this.http.post<any>(endpoint, this.sentence, { reportProgress: true });
  }

  analyzeButtonClicked() {

  }
}
