import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { StocksComponent } from './stocks.component';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';
import { TagModule } from 'primeng/tag';

@NgModule({
  declarations: [
    StocksComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    ButtonModule,
    PaginatorModule,
    TagModule,
  ],
  exports: [
    StocksComponent
  ]
})
export class StocksModule { }
