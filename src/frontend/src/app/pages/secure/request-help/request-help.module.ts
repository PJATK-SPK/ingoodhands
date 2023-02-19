import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { RequestHelpComponent } from './request-help.component';

@NgModule({
  declarations: [
    RequestHelpComponent
  ],
  imports: [
    CommonModule,
    CardModule,
  ],
  exports: [
    RequestHelpComponent
  ]
})
export class RequestHelpModule { }
