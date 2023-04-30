import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { GoogleMapsModule } from '@angular/google-maps';
import { String2DateModule } from 'src/app/pipes/string2date/string2date.module';
import { ButtonModule } from 'primeng/button';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { TagModule } from 'primeng/tag';
import { CurrentDeliveryComponent } from './current-delivery.component';

@NgModule({
  declarations: [
    CurrentDeliveryComponent,
  ],
  imports: [
    CommonModule,
    CardModule,
    String2DateModule,
    GoogleMapsModule,
    ButtonModule,
    ConfirmPopupModule,
    TagModule,
  ],
  exports: [
    CurrentDeliveryComponent
  ],
})
export class CurrentDeliveryModule { }
