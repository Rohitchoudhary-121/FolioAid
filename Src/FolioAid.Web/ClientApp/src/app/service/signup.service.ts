import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SignupService {
  private baseUrl: string = "https://localhost:44480/api/Account/";
  constructor(private http: HttpClient) { }
  signUp(userObj: any) {
    return this.http.post<any>(`${this.baseUrl}RegisterUser`, userObj);
  }

  activateString(email: string,activeString: any) {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    const body = {
      email: email,
      activationString: activeString
    };

    return this.http.post<any>(`${this.baseUrl}ActivateAccount`, body, { headers });
  }

}
