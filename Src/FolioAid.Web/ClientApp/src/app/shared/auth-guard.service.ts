import { Injectable } from '@angular/core';
import { LoginservicesService } from '../service/loginservices.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService {

  constructor(private loginSvc: LoginservicesService, private router: Router) { }
  canActivate() {
   const res= this.loginSvc.isAuthenticated();
   if(res)
     return true;
  else
  {
    this.router.navigateByUrl('');
    return false;
  }
    
  }
}
