import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyDonationsComponent } from './my-donations.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';

@NgModule({
  declarations: [
    MyDonationsComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    ButtonModule,
  ],
  exports: [
    MyDonationsComponent
  ]
})
export class MyDonationsModule { }
