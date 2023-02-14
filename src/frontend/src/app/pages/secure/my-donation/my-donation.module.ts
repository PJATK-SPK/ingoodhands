import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { MyDonationComponent } from './my-donation.component';

@NgModule({
  declarations: [
    MyDonationComponent
  ],
  imports: [
    CommonModule,
    CardModule
  ],
  exports: [
    MyDonationComponent
  ]
})
export class MyDonationModule { }
