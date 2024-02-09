import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import ValidateForm from 'src/app/helpers/validateForm';
import { Router } from '@angular/router';
import { MessageService } from 'src/app/service/message.service';
import { LoginservicesService } from '../../service/loginservices.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  signUpForm!: FormGroup;
  type: string = "password";
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash"
  passwordsMatching = false;

  progressValue: number = 0;
  formsubmit: boolean = false;
  isButtonDisabled: boolean = false;
  
  constructor(private fb: FormBuilder, private sign: LoginservicesService, private router: Router, private messageSvc: MessageService, private toastr: ToastrService) { }


  // click on eye icon and show or hide password

  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }

  ngOnInit(): void {
    this.signUpForm = this.fb.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', Validators.required]
    },
      { validators: this.MustMatch('password', 'confirmPassword') });

  }
  //function to match confirmpassword

  MustMatch(controlName: string, matchingControlName: string) {
    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];
      if (
        matchingControl.errors &&
        !matchingControl.errors.MustMatch
      ) {
        return;
      }
      if (control.value !== matchingControl.value) {
        matchingControl.setErrors({ mustMatch: true });
      } else {
        matchingControl.setErrors(null);
      }
    }
  };

  onSignup() {
    const formData = { ...this.signUpForm.value };
    delete formData.confirmPassword; // Remove confirmPassword from form data
    if (this.signUpForm.valid) {
      this.isButtonDisabled = true;
      this.formsubmit = true;
      this.progressValue = 20;
      for (var i = 1; i < 4; i++) {
        setTimeout(() => {

          this.progressValue += 20;
        }, 700);
      }
      this.sign.signUp(formData)
        .subscribe({
          next: (res) => {
            const toast = this.toastr.success('Signup successfully , Please actiavte your account to continue', '', {
              closeButton: true,
              tapToDismiss: false,
              disableTimeOut: true,
              positionClass: 'toast-top-full-width',
            });
            this.signUpForm.reset();
            this.progressValue = 140;
            toast.onHidden.subscribe(() => {
              this.router.navigate(['userlogin']);
            });
          },
          error: (err) => {
            this.isButtonDisabled = false;
            this.progressValue = 100;
            setTimeout(() => {
     
              this.toastr.error(err.error.message, '', {
                closeButton: true,
                tapToDismiss: false,
                disableTimeOut: true,
                positionClass: 'toast-top-full-width'
              });
            this.formsubmit = false;
            
            }, 500);
          }
        })
    }
    else {
      this.signUpForm.markAllAsTouched();
      return;
    }

    
  }

}
