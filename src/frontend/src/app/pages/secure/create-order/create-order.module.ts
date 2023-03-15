import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { CreateOrderComponent } from './create-order.component';
import { ReactiveFormsModule } from '@angular/forms';
import { StyleClassModule } from 'primeng/styleclass';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { RadioButtonModule } from 'primeng/radiobutton';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    CreateOrderComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    RouterModule,
    ReactiveFormsModule,
    StyleClassModule,
    InputTextModule,
    AutoCompleteModule,
    RadioButtonModule,
    ButtonModule,
  ],
  exports: [
    CreateOrderComponent
  ]
})
export class CreateOrderModule { }
