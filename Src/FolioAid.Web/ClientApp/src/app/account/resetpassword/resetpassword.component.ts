import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginservicesService } from '../../service/loginservices.service';
import { MessageService } from '../../service/message.service';





@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.css']
})
export class ResetpasswordComponent implements OnInit {
  userform: FormGroup;
  data: any;
  result: any;
  message: any;
  repeatpass: string = 'none'

  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash"
  passwordsMatching = false;
  hideShowPass() {

    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye-slash" : this.eyeIcon = "fa-eye";
    this.isText ? this.type = "password" : this.type = "text";
  }

  constructor(private route: ActivatedRoute, private resetpasswordservice: LoginservicesService, private router: Router, private messageSvc: MessageService, private toastr: ToastrService) {
    this.userform = new FormGroup({
      password: new FormControl("", [Validators.required, Validators.minLength(8), Validators.maxLength(12)]),
      rptpassword: new FormControl("", [Validators.required])
    });
    
  }
 /* this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";*/

  get Password(): FormControl {
    return this.userform.get("password") as FormControl
  }
  get RepeatPassword(): FormControl {
    return this.userform.get("rptpassword") as FormControl
  }
  email: string;
  token: string;
  ngOnInit(): void {
    this.route.queryParams
      .subscribe(params => {
        console.log(params); 
        this.email = params.email;
        this.token = params.token;
      }
    );
   
  }

  updatedata() {
    if (this.userform.valid) {
      if (this.RepeatPassword.value == this.Password.value) {
        let body = {
          newPassword: this.userform.value.password,
          email: this.email,
          token: this.token
        }
        this.resetpasswordservice.updateuserpassword(body)
          .subscribe({
            next: (res) => {
              this.toastr.success('Password changed');
              this.router.navigate(['/userlogin'])
            },
            error: (err) => {
              this.toastr.error(err.error.message);
            }
          }
          );
      }
      else {
        this.repeatpass = 'inline'
      }



    }
    else {
      this.userform.markAllAsTouched();
    }

  }

}
