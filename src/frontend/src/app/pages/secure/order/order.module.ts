import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { OrderComponent } from './order.component';
import { GoogleMapsModule } from '@angular/google-maps';
import { String2DateModule } from 'src/app/pipes/string2date/string2date.module';
import { ButtonModule } from 'primeng/button';
import { ConfirmPopupModule } from 'primeng/confirmpopup';

@NgModule({
  declarations: [
    OrderComponent,
  ],
  imports: [
    CommonModule,
    CardModule,
    String2DateModule,
    GoogleMapsModule,
    ButtonModule,
    ConfirmPopupModule,
  ],
  exports: [
    OrderComponent
  ],
})
export class OrderModule { }
