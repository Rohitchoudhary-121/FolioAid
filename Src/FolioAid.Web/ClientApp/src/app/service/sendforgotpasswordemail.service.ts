import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SendforgotpasswordemailService {
  constructor(private http: HttpClient) { }
  baseServerUrl = "https://localhost:44480/api/Account/SendForgotPasswordEmail";

  sendmail(body: any): Observable<Object> {
    return this.http.post(this.baseServerUrl, {
      Email: body[0],
    });

  }
  
}
