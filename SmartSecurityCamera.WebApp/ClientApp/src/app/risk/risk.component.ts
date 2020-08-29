import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-risk',
  templateUrl: './risk.component.html',
})
export class RiskComponent {
  fileToUpload: File = null;
  public message: string;
  public issue: string;
  public type: string;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  postSentence(): Observable<any> {
    let endpoint: string;
    console.log(this.issue);
    if (!!this.baseUrl) {
      endpoint = 'https://localhost:5001/api/ItemClassification';
    } else {
      endpoint = 'https://objectdetectionwithtraining-dev-as.azurewebsites.net/api/Yolo';
    }
    return this.http.post<any>(endpoint, { issue: this.issue, tpye: this.type }, { reportProgress: true });
  }

  analyzeButtonClicked() {
    this.postSentence().subscribe(result => {
      console.log(result);
      this.message = "The risk is: " + result.result.prediction;
    });
  }
}
