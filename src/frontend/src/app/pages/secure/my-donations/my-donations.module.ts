import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyDonationsComponent } from './my-donations.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';
import { FormsModule } from '@angular/forms';
import { TooltipModule } from 'primeng/tooltip';
import { RouterModule } from '@angular/router';
import { TagModule } from 'primeng/tag';

@NgModule({
  declarations: [
    MyDonationsComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    ButtonModule,
    PaginatorModule,
    FormsModule,
    TooltipModule,
    RouterModule,
    TagModule,
  ],
  exports: [
    MyDonationsComponent
  ]
})
export class MyDonationsModule { }
