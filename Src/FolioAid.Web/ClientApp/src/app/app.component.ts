import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginservicesService } from './service/loginservices.service';
import { MessageService } from 'src/app/service/message.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  constructor(private route: ActivatedRoute, private loginservices: LoginservicesService, private messageSvc: MessageService, private router: Router, private toastr: ToastrService){}
  title = 'app';
  ActivationString: any;
  ngOnInit(){
    // Accessing the ActivationString value
    var url=window.location.href;
    this.ActivationString = url.split('activate=')[1];
    console.log(this.ActivationString);
    if (this.ActivationString != null) {
      this.activateAccount();
    }
  }
  
  activateAccount() {
    this.loginservices.activateString(this.ActivationString)
      .subscribe({
        next: (res) => {
          this.router.navigateByUrl('activateaccount');
        },
        error: (err) => {
          this.toastr.error(err.error.message, '', {
            closeButton: true,
            tapToDismiss: false,
            disableTimeOut: true,
            positionClass: 'toast-top-full-width'
          });
          this.router.navigateByUrl('userlogin');
        }
      })
  }
}
