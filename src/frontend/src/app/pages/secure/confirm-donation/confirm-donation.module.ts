import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmDonationComponent } from './confirm-donation.component';
import { CardModule } from 'primeng/card';

@NgModule({
  declarations: [
    ConfirmDonationComponent
  ],
  imports: [
    CommonModule,
    CardModule
  ],
  exports: [
    ConfirmDonationComponent
  ]
})
export class ConfirmDonationModule { }
