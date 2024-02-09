import { Component } from '@angular/core';
import { LoginservicesService } from '../service/loginservices.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  loggedIn:boolean;
  isExpanded = false;
  fullName: any;
  constructor( private loginService:LoginservicesService, private router : Router){
    this.checkLoggedIn();
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  checkLoggedIn(){
    this.loginService.checkLoggedIn.subscribe(res => this.loggedIn = res)
    this.loginService.getNameFromStore().subscribe(val => {
      var Name = this.loginService.getFullNameFromToken();
      this.fullName = val||Name;
    })
   
  }
  logOut(){
    localStorage.removeItem("token");
    this.loginService.setLoggedIn(false);
    this.router.navigateByUrl('');
  }
 
}
