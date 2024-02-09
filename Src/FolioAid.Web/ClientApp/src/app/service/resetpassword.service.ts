import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ResetpasswordService {
  constructor(private http: HttpClient) { }
  baseServerUrl = "https://localhost:44480/api/Account/ResetPassword";


  updateuserpassword(body: any): Observable<Object> {
    return this.http.put(this.baseServerUrl, body);
  }
 
}
