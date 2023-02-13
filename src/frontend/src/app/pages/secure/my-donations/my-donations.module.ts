import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyDonationsComponent } from './my-donations.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';

@NgModule({
  declarations: [
    MyDonationsComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    ButtonModule,
    PaginatorModule
  ],
  exports: [
    MyDonationsComponent
  ]
})
export class MyDonationsModule { }
