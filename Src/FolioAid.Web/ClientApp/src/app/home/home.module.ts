import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeRoutingModule } from './home.routing.module';
import { HomeComponent } from './home/home.component';
import { PortfolioModule } from '../portfolio/portfolio.module';



@NgModule({
  declarations: [
   HomeComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    PortfolioModule
  ]
})
export class HomeModule { }
