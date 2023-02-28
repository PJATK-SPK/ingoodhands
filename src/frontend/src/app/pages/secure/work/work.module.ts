import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { WorkComponent } from './work.component';

@NgModule({
  declarations: [
    WorkComponent
  ],
  imports: [
    CommonModule,
    CardModule,
  ],
  exports: [
    WorkComponent
  ]
})
export class WorkModule { }
