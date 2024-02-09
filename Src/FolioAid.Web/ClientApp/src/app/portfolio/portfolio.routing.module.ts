import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { PortfolioListingComponent } from "./portfolio-listing/portfolio-listing.component";
import { AddprojectComponent } from "./addproject/addproject.component";
import { ProjectlistingpublicviewComponent } from "./projectlistingpublicview/projectlistingpublicview.component";
import { ProjectDetailComponent } from "./project-detail/project-detail.component";
import { AuthGuardService } from "../shared/auth-guard.service";

const routes:Routes=[
   {
    path: 'add-project',
    canActivate: [AuthGuardService],
    component:AddprojectComponent
  },
  {
    path: 'portfolio', 
    component: ProjectlistingpublicviewComponent
  },
   {
    path:'project-listing',
    canActivate: [AuthGuardService],
    component:PortfolioListingComponent
   },
   {
    path:'update-project/:id',
    canActivate: [AuthGuardService],
    component:AddprojectComponent
  },
  {
    path:'project-detail/:id',
    component:ProjectDetailComponent
 }
];

@NgModule({
    declarations: [],
    imports: [RouterModule.forChild(routes)],
    exports:[RouterModule]
  })
 
  export class PortfolioRoutingModule{}
