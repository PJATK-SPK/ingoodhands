import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';

@NgModule({
  declarations: [
    MainComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    ButtonModule,
  ],
  exports: [
    MainComponent
  ]
})
export class MainModule { }
