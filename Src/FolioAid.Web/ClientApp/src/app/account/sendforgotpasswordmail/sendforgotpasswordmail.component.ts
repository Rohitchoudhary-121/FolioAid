import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginservicesService } from '../../service/loginservices.service';
import { MessageService } from '../../service/message.service';

@Component({
  selector: 'app-sendforgotpasswordmail',
  templateUrl: './sendforgotpasswordmail.component.html',
  styleUrls: ['./sendforgotpasswordmail.component.css']
})
export class SendforgotpasswordmailComponent {
  data: any;
  result: any;
  message: any;
  constructor(private sendforgotpasswordemailService: LoginservicesService, private messageSvc: MessageService, private router: Router, private toastr: ToastrService) {

  }
  userform = new FormGroup({
    email: new FormControl("", [Validators.required, Validators.email]),

  });

  get UserEmail(): FormControl {
    return this.userform.get("email") as FormControl
  }
  onFormSubmit() {
    if (this.userform.valid) {
      this.sendforgotpasswordemailService.sendmail([
        this.userform.value.email
      ]
      ).subscribe({
        next: (res) => {
          this.toastr.success('Send link on Your Mail');
          this.router.navigate(['/userlogin'])
        },
        error: (err) => {
          this.toastr.error(err.error.message);
        }
      }
      )
    }
     else {
      this.userform.markAllAsTouched();
    }


  }



   

}
