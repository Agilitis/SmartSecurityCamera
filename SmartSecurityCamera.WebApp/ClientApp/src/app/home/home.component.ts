import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { WebcamImage } from 'ngx-webcam';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  fileToUpload: File = null;
  public result: string;
  isWeaponDetected = false;
  // webcam snapshot trigger
  private trigger: Subject<void> = new Subject<void>();
  private isAnalizationOn = false;
  // latest snapshot
  public webcamImage: WebcamImage = null;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    setInterval(() => {
      if (this.isAnalizationOn) {
        this.trigger.next();
      }
    }, 500);
  }

  public triggerSnapshot(): void {
    this.isAnalizationOn = !this.isAnalizationOn;
  }

  public handleImage(webcamImage: WebcamImage): void {
    this.webcamImage = webcamImage;
    const imageName = 'asdf' + '.jpeg';
    // call method that creates a blob from dataUri
    const imageBlob = this.dataURItoBlob(this.webcamImage.imageAsBase64);
    const imageFile = new File([imageBlob], imageName, { type: 'image/jpeg' });
    this.postFile(imageFile).subscribe(result => {
      this.isWeaponDetected = result > 0.98;
    });
  }

  postFile(fileToUpload: File): Observable<any> {
    let endpoint: string;
    if (!!this.baseUrl) {
      endpoint = 'https://localhost:5001/api/WeaponDetection';
    } else {
      endpoint = "https://objectdetectionwithtraining-dev-as.azurewebsites.net/api/Videos";
    }
    const formData: FormData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    return this.http.post<any>(endpoint, formData, { reportProgress: true });
  }

  public get triggerObservable(): Observable<void> {
    return this.trigger.asObservable();
  }

  dataURItoBlob(dataURI) {
    const byteString = window.atob(dataURI);
    const arrayBuffer = new ArrayBuffer(byteString.length);
    const int8Array = new Uint8Array(arrayBuffer);
    for (let i = 0; i < byteString.length; i++) {
      int8Array[i] = byteString.charCodeAt(i);
    }
    const blob = new Blob([int8Array], { type: 'image/jpeg' });
    return blob;
  }
}
