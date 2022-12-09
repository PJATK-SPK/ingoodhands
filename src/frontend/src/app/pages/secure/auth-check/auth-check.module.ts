import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthCheckComponent } from './auth-check.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';

@NgModule({
  declarations: [
    AuthCheckComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    ButtonModule,
  ],
  exports: [
    AuthCheckComponent
  ]
})
export class AuthCheckModule { }
