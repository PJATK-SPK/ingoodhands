import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { CreateOrderComponent } from './create-order.component';

@NgModule({
  declarations: [
    CreateOrderComponent
  ],
  imports: [
    CommonModule,
    CardModule,
  ],
  exports: [
    CreateOrderComponent
  ]
})
export class CreateOrderModule { }
