import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageUsersComponent } from './manage-users.component';
import { CardModule } from 'primeng/card';

@NgModule({
  declarations: [
    ManageUsersComponent
  ],
  imports: [
    CommonModule,
    CardModule,
  ],
  exports: [
    ManageUsersComponent
  ]
})
export class ManageUsersModule { }
