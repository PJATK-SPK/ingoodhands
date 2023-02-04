import { Component } from '@angular/core';
import { MenuItem, MessageService } from 'primeng/api';
import { StepperService } from './services/stepper.service';
import { Router } from '@angular/router';
import { Step1Service } from './services/step-1.service';
import { Step2Service } from './services/step-2.service';

@Component({
  selector: 'app-donate-stepper',
  templateUrl: './donate-stepper.component.html',
  styleUrls: ['./donate-stepper.component.scss'],
  providers: [
    StepperService,
    Step1Service,
    Step2Service,
  ],
})
export class DonateStepperComponent {

  constructor(
    public readonly service: StepperService,
    private readonly router: Router,
    private readonly msg: MessageService
  ) { }

  public steps: MenuItem[] = [
    { label: 'Your donation' },
    { label: 'Collect destination' },
    { label: 'Confirmation' },
  ];

  public onNextClick() {
    if (this.hasErrors()) {
      return;
    }

    const currentStep = this.service.currentIndex;

    this.moveForward();

    if (currentStep === this.steps.length - 1) {
      this.goToApp();
    }
  }

  public onBackClick() {
    this.moveBackward();
  }

  private goToApp() {
    this.service.step1.onClearClick();
    this.service.step1.save();
    this.router.navigateByUrl('/secure');
  }

  private hasErrors() {
    if (this.service.currentIndex === 0) {
      this.service.step1.save();
      const error = this.service.step1.hasError();

      if (error) {
        this.msg.add({ severity: 'error', summary: 'Products form', detail: error });
        return true;
      }
    }

    if (this.service.currentIndex === 1) {
      const error = this.service.step2.hasError();

      if (error) {
        this.msg.add({ severity: 'error', summary: 'Warehouse selection', detail: error });
        return true;
      }
    }

    return false;
  }

  private moveForward() {
    if (this.service.currentIndex < this.steps.length - 1) {
      this.service.currentIndex++;
    }
  }

  private moveBackward() {
    if (this.service.currentIndex >= 0) {
      this.service.currentIndex--;
    }
  }
}
