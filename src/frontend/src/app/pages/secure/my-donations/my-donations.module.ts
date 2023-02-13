import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyDonationsComponent } from './my-donations.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule } from '@angular/forms';
import { TooltipModule } from 'primeng/tooltip';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    MyDonationsComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    ButtonModule,
    PaginatorModule,
    CheckboxModule,
    FormsModule,
    TooltipModule,
    RouterModule
  ],
  exports: [
    MyDonationsComponent
  ]
})
export class MyDonationsModule { }
