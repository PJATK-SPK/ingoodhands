import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { CreateAddressComponent } from './create-address.component';
import { ReactiveFormsModule } from '@angular/forms';
import { StyleClassModule } from 'primeng/styleclass';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { GoogleMapsModule } from '@angular/google-maps';

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
    GoogleMapsModule,
    ButtonModule,
  ],
  exports: [
    CreateAddressComponent
  ]
})
export class CreateAddressModule { }
