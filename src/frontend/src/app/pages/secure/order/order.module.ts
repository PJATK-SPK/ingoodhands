import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { OrderComponent } from './order.component';

@NgModule({
  declarations: [
    OrderComponent
  ],
  imports: [
    CommonModule,
    CardModule,
  ],
  exports: [
    OrderComponent
  ]
})
export class OrderModule { }
