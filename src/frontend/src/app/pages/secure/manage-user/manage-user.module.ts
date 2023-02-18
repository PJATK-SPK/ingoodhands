import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageUserComponent } from './manage-user.component';
import { CardModule } from 'primeng/card';

@NgModule({
  declarations: [
    ManageUserComponent
  ],
  imports: [
    CommonModule,
    CardModule,
  ],
  exports: [
    ManageUserComponent
  ]
})
export class ManageUserModule { }
