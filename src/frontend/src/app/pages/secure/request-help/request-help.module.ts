import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { RequestHelpComponent } from './request-help.component';
import { GoogleMapsModule } from '@angular/google-maps';
import { ButtonModule } from 'primeng/button';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    RequestHelpComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    GoogleMapsModule,
    ButtonModule,
    RouterModule,
  ],
  exports: [
    RequestHelpComponent
  ]
})
export class RequestHelpModule { }
