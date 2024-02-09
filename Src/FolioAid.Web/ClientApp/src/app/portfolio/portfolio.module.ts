import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatPaginatorModule } from '@angular/material/paginator';
import { GridModule } from '@progress/kendo-angular-grid';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { PortfolioRoutingModule } from './portfolio.routing.module';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { InputsModule, SliderModule } from '@progress/kendo-angular-inputs';
import { AddprojectComponent } from './addproject/addproject.component';
import { PortfolioListingComponent } from './portfolio-listing/portfolio-listing.component';
import { ProjectlistingpublicviewComponent } from './projectlistingpublicview/projectlistingpublicview.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DialogsModule } from '@progress/kendo-angular-dialog';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgImageSliderModule } from 'ng-image-slider';
import { ProgressModule } from '@coreui/angular';


@NgModule({
  declarations: [AddprojectComponent, PortfolioListingComponent, ProjectlistingpublicviewComponent, ProjectDetailComponent],
  imports: [
    CommonModule,
    MatPaginatorModule,
    GridModule,
    ButtonsModule,
    FormsModule,
    PortfolioRoutingModule,
    ReactiveFormsModule,
    DropDownsModule,
    InputsModule,
    CommonModule,
    RouterModule,
    DialogsModule,
    NgbModule,
    NgImageSliderModule,
    ProgressModule
  ],
  exports: [
    ]
})
export class PortfolioModule { }
