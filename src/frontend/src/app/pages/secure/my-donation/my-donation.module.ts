import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { MyDonationComponent } from './my-donation.component';
import { TagModule } from 'primeng/tag';
import { GoogleMapsModule } from '@angular/google-maps'

@NgModule({
  declarations: [
    MyDonationComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    TagModule,
    GoogleMapsModule,
  ],
  exports: [
    MyDonationComponent
  ]
})
export class MyDonationModule { }
