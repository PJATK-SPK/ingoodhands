import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';
import { FormsModule } from '@angular/forms';
import { TooltipModule } from 'primeng/tooltip';
import { RouterModule } from '@angular/router';
import { TagModule } from 'primeng/tag';
import { InputTextModule } from 'primeng/inputtext';
import { AvailableDeliveriesComponent } from './available-deliveries.component';

@NgModule({
  declarations: [
    AvailableDeliveriesComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    ButtonModule,
    PaginatorModule,
    FormsModule,
    InputTextModule,
    TooltipModule,
    RouterModule,
    TagModule,
  ],
  exports: [
    AvailableDeliveriesComponent
  ]
})
export class AvailableDeliveriesModule { }
