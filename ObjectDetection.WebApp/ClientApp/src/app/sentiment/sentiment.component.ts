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

  postSentence(): Observable<any> {
    let endpoint: string;
    console.log(this.sentence);
    if (!!this.baseUrl) {
      endpoint = 'https://localhost:5001/api/Sentiment';
    } else {
      endpoint = 'https://objectdetectionwithtraining-dev-as.azurewebsites.net/api/Yolo';
    }
    return this.http.post<any>(endpoint, { sentence: this.sentence }, { reportProgress: true });
  }

  analyzeButtonClicked() {
    this.postSentence().subscribe(result => {
      console.log(result);
      if (result.prediction) {
        this.message = 'A mondat pozitív tartalmú ' + result.score + ' ponttal.';
      } else {
        this.message = 'A mondat negatív tartalmú ' + result.score + ' ponttal.';
      }
    });
  }
}
