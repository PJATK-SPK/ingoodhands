import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { StocksComponent } from './stocks.component';

@NgModule({
  declarations: [
    StocksComponent
  ],
  imports: [
    CommonModule,
    CardModule,
  ],
  exports: [
    StocksComponent
  ]
})
export class StocksModule { }
