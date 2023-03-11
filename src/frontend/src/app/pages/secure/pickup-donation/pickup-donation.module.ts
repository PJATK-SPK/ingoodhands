import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { PickupDonationComponent } from './pickup-donation.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { StyleClassModule } from 'primeng/styleclass';

@NgModule({
  declarations: [
    PickupDonationComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    ReactiveFormsModule,
    ButtonModule,
    InputTextModule,
    StyleClassModule,
  ],
  exports: [
    PickupDonationComponent
  ]
})
export class PickUpDonationModule { }
