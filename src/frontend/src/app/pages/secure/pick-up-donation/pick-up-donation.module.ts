import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { PickUpDonationComponent } from './pick-up-donation.component';

@NgModule({
  declarations: [
    PickUpDonationComponent
  ],
  imports: [
    CommonModule,
    CardModule,
  ],
  exports: [
    PickUpDonationComponent
  ]
})
export class PickUpDonationModule { }
