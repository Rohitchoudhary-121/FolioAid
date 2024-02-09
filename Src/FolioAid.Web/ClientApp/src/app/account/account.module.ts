import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserloginComponent } from './userlogin/userlogin.component';
import { AccountRoutingModule } from './account.routing.module';
import { SignUpComponent } from './sign-up/sign-up.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ResetpasswordComponent } from './resetpassword/resetpassword.component';
import { SendforgotpasswordmailComponent } from './sendforgotpasswordmail/sendforgotpasswordmail.component';
import { ProgressModule } from '@coreui/angular';
import { ActivateaccountconfirmComponent } from './activateaccountconfirm/activateaccountconfirm.component';


@NgModule({
  declarations: [
    UserloginComponent,
    SignUpComponent,
    ResetpasswordComponent,
    SendforgotpasswordmailComponent,
    ActivateaccountconfirmComponent
    
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    ProgressModule
  ]
})
export class AccountModule { }
