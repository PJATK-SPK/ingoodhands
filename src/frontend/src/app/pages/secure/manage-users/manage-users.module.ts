import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageUsersComponent } from './manage-users.component';
import { CardModule } from 'primeng/card';
import { RouterModule } from '@angular/router';
import { TagModule } from 'primeng/tag';
import { PaginatorModule } from 'primeng/paginator';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { StyleClassModule } from 'primeng/styleclass';

@NgModule({
  declarations: [
    ManageUsersComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    RouterModule,
    TagModule,
    PaginatorModule,
    InputTextModule,
    FormsModule,
    StyleClassModule,
  ],
  exports: [
    ManageUsersComponent
  ]
})
export class ManageUsersModule { }
