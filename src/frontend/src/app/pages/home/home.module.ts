import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { StyleClassModule } from 'primeng/styleclass';
import { TabViewModule } from 'primeng/tabview';
import { NavbarComponent } from './navbar/navbar.component';
import { DonateComponent } from './donate/donate.component';
import { RequestHelpComponent } from './request-help/request-help.component';
import { WorkComponent } from './work/work.component';
import { ButtonModule } from 'primeng/button';
import { SplashImgComponent } from './splash-img/splash-img.component';
import { DonateStepperModule } from './donate/stepper/donate-stepper.module';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    HomeComponent,
    NavbarComponent,
    SplashImgComponent,
    DonateComponent,
    RequestHelpComponent,
    WorkComponent,
  ],
  imports: [
    CommonModule,
    ButtonModule,
    StyleClassModule,
    TabViewModule,
    DonateStepperModule,
    RouterModule,
  ],
  exports: [
    HomeComponent
  ]
})
export class HomeModule { }
