import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { UserloginComponent } from "./userlogin/userlogin.component";
import { SignUpComponent } from "./sign-up/sign-up.component";
import { ResetpasswordComponent } from "./resetpassword/resetpassword.component";
import { SendforgotpasswordmailComponent } from "./sendforgotpasswordmail/sendforgotpasswordmail.component";
import { AuthGuardService } from "../shared/auth-guard.service";
import { ActivateaccountconfirmComponent } from "./activateaccountconfirm/activateaccountconfirm.component";


const routes: Routes = [
  {
    path: 'userlogin', 
    component: UserloginComponent, pathMatch: 'full'
  },
  { path: 'resetpassword', component: ResetpasswordComponent },
  { path: 'sendmailforgotpassword', component: SendforgotpasswordmailComponent },
   {
     path: 'sign-up',
   
    component:SignUpComponent
  },
  { path: "activateaccount", component: ActivateaccountconfirmComponent }
];

@NgModule({
    declarations: [],
    imports: [RouterModule.forChild(routes)],
    exports:[RouterModule]
  })
 
  export class AccountRoutingModule{}
