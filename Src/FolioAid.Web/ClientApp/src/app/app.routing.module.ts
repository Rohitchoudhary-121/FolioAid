import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CounterComponent } from "./counter/counter.component";
import { FetchDataComponent } from "./fetch-data/fetch-data.component";
import { HomeLayoutComponent } from "./Layouts/home/home.layout.component";
import { SignUpComponent } from "./account/sign-up/sign-up.component";


const routes:Routes=[  
    { path: 'counter', component: CounterComponent },
    { path: 'fetch-data', component: FetchDataComponent },
    {
      path: '',
      component: HomeLayoutComponent,
          loadChildren: () => import('./home/home.module').then(m=>m.HomeModule)
    },
    { path:'account', loadChildren: () => import('./account/account.module').then(m => m.AccountModule) },
    { path:'portfolio', loadChildren: () => import('./portfolio/portfolio.module').then(m => m.PortfolioModule) },
  ];
@NgModule({
    declarations: [],
    imports: [RouterModule.forRoot(routes)],
    exports:[RouterModule]
  })
 
  export class AppRoutingModule{}