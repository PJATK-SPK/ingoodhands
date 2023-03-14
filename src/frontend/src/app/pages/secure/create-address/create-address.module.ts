import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { CreateAddressComponent } from './create-address.component';
import { ReactiveFormsModule } from '@angular/forms';
import { StyleClassModule } from 'primeng/styleclass';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { AutoCompleteModule } from 'primeng/autocomplete';

@NgModule({
  declarations: [
    CreateAddressComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    ReactiveFormsModule,
    StyleClassModule,
    InputTextModule,
    AutoCompleteModule,
    ButtonModule,
  ],
  exports: [
    CreateAddressComponent
  ]
})
export class CreateAddressModule { }
