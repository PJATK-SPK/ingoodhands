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
    StepsModule,
  ],
  exports: [
    DonateStepperComponent,
  ]
})
export class DonateStepperModule { }
