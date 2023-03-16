import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { OrderComponent } from './order.component';
import { GoogleMapsModule } from '@angular/google-maps';
import { String2DateModule } from 'src/app/pipes/string2date/string2date.module';

@NgModule({
  declarations: [
    OrderComponent,
  ],
  imports: [
    CommonModule,
    CardModule,
    String2DateModule,
    GoogleMapsModule,
  ],
  exports: [
    OrderComponent
  ]
})
export class OrderModule { }
