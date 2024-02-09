import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { LoginservicesService } from '../../service/loginservices.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private loginSvc: LoginservicesService, private router: Router, private location: Location) { }
    ngOnInit(): void {
      const res = this.loginSvc.isAuthenticated();
      if (res) {
        this.router.navigate(['/project-listing']);
      
      }
    }

}
