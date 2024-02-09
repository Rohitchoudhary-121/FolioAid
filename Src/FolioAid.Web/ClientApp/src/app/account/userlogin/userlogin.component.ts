import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LoginservicesService } from '../../service/loginservices.service';
import { MessageService } from 'src/app/service/message.service';
import { ActivatedRoute, ParamMap, Route, Router } from '@angular/router';
import { switchMap } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';

import { MatDialog } from '@angular/material/dialog';
import { SendforgotpasswordmailComponent } from '../sendforgotpasswordmail/sendforgotpasswordmail.component';
import { ToastrService } from 'ngx-toastr';
const headers = new HttpHeaders({
  'Content-Type': 'application/json'
});

@Component({
  selector: 'app-userlogin',
  templateUrl: './userlogin.component.html',
  styleUrls: ['./userlogin.component.css']
})
export class UserloginComponent implements OnInit {
  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash"
  passwordsMatching = false;

  isButtonDisabled: boolean = false;
 
  progressValue: number = 0;
  formsubmit: boolean = false;
  
  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }
  constructor(private loginservices: LoginservicesService, private messageSvc: MessageService, private router: Router, private dialog: MatDialog, private toastr: ToastrService) {
    this.loginservices.setLoggedIn(false);
  }

  ngOnInit(){}

  get UserEmail(): FormControl {
    return this.loginform.get("useremail") as FormControl
  }
  get UserPassword(): FormControl {
    return this.loginform.get("password") as FormControl
  }
  

  loginform = new FormGroup({
    useremail: new FormControl("", [Validators.required, Validators.email]),
    password: new FormControl("", [Validators.required, Validators.minLength(8)]),
   
  });
  isUserValid: boolean = false;

  loginsubmited() {
    if (this.loginform.valid) {
      this.isButtonDisabled = true;
      this.formsubmit = true;
      this.progressValue = 20;
      for (var i = 1; i < 4; i++) {
        setTimeout(() => {
          this.progressValue += 20;
        }, 700);
      }


      this.loginservices.userlogin([
        this.loginform.value.useremail,
        this.loginform.value.password
       
      ]).subscribe({
        next: (res) => {
          
          this.progressValue = 140;
         
          localStorage.setItem('token', res.result.token);
          const tokenPayload = this.loginservices.decodedToken();
          this.loginservices.setNameFromStore(tokenPayload.unique_name);
          setTimeout(() => {
           
            this.toastr.success('Log in successfully','',{
              closeButton: true,
            });
            this.loginservices.setLoggedIn(true);
            this.router.navigate(['project-listing'])
          }, 300);


        },
        error: (err) => {
          this.isButtonDisabled = false;
          this.progressValue = 100;
          setTimeout(() => {
            this.toastr.error(err.error.message, '', {
              closeButton: true,
            });
            this.formsubmit = false;

          }, 500);
        }
      }
      )
    }
    else {
      this.loginform.markAllAsTouched();

    }
  }

  }




