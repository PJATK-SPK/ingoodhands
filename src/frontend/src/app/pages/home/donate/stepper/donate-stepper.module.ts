import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StyleClassModule } from 'primeng/styleclass';
import { TabViewModule } from 'primeng/tabview';
import { ButtonModule } from 'primeng/button';
import { StepsModule } from 'primeng/steps';
import { DonateStepperComponent } from './donate-stepper.component';
import { DonateStep1Component } from './steps/step-1/donate-step-1.component';
import { DonateStep2Component } from './steps/step-2/donate-step-2.component';
import { DonateStep3Component } from './steps/step-3/donate-step-3.component';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { SelectButtonModule } from 'primeng/selectbutton';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { RadioButtonModule } from 'primeng/radiobutton';

@NgModule({
  declarations: [
    DonateStepperComponent,
    DonateStep1Component,
    DonateStep2Component,
    DonateStep3Component,
  ],
  imports: [
    CommonModule,
    ButtonModule,
    StyleClassModule,
    TabViewModule,
    FormsModule,
    StepsModule,
    SelectButtonModule,
    InputTextModule,
    AutoCompleteModule,
    RadioButtonModule,
  ],
  exports: [
    DonateStepperComponent,
  ]
})
export class DonateStepperModule { }
