import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageUserComponent } from './manage-user.component';
import { CardModule } from 'primeng/card';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { ReactiveFormsModule } from '@angular/forms';
import { InputSwitchModule } from 'primeng/inputswitch';
import { ButtonModule } from 'primeng/button';

@NgModule({
  declarations: [
    ManageUserComponent
  ],
  imports: [
    CommonModule,
    CardModule,
    AutoCompleteModule,
    ReactiveFormsModule,
    InputSwitchModule,
    ButtonModule,
  ],
  exports: [
    ManageUserComponent
  ]
})
export class ManageUserModule { }
