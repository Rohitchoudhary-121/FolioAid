import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { TokenInterceptorService } from './service/token-interceptor.service';
import { AppRoutingModule } from './app.routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AccountModule } from './account/account.module';
import { HomeLayoutComponent } from './Layouts/home/home.layout.component';
import { HomeModule } from './home/home.module';
import { NotificationModule } from '@progress/kendo-angular-notification';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { CounterComponent } from './counter/counter.component';
/*import { SendforgotpasswordmailComponent } from './account/sendforgotpasswordmail/sendforgotpasswordmail.component';*/
import { MatDialogModule } from '@angular/material/dialog';
import { ProgressModule } from '@coreui/angular';
import { IconModule, IconSetService } from '@coreui/icons-angular';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FetchDataComponent,
    CounterComponent,
    HomeLayoutComponent,
   /* SendforgotpasswordmailComponent,*/
   
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule ,
    HttpClientModule,
    AppRoutingModule,
    AccountModule,
    HomeModule,
    NotificationModule,
    MatDialogModule,
    ProgressModule,
    IconModule,
    ToastrModule.forRoot({
      preventDuplicates:true
    }),
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true
  },
    IconSetService
  ],

  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }
