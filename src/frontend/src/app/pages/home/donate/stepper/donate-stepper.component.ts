import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { StepperService } from './services/stepper.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-donate-stepper',
  templateUrl: './donate-stepper.component.html',
  styleUrls: ['./donate-stepper.component.scss'],
  providers: [StepperService],
})
export class DonateStepperComponent implements OnInit {

  constructor(
    public readonly service: StepperService,
    private readonly router: Router,
  ) { }

  public steps: MenuItem[] = [
    {
      label: 'Your donation',
    },
    {
      label: 'Collect destination',
    },
    {
      label: 'Confirmation',
    },
  ];

  public currentIndex = 0;

  public ngOnInit(): void {
    this.service.load();
  }

  public onNextClick() {
    const currentStep = this.service.currentStep;

    if (this.currentIndex < this.steps.length - 1) {
      this.currentIndex++;
    }

    this.updateServiceStep();
    this.service.save();

    if (currentStep === 3) {
      this.service.clear();
      this.service.save();
      this.router.navigateByUrl('/secure');
    }
  }

  public onBackClick() {
    if (this.currentIndex >= 0) {
      this.currentIndex--;
    }

    this.updateServiceStep();
  }

  public onAddClick() {

  }

  private updateServiceStep() {
    this.service.currentStep = this.currentIndex + 1 as 1 | 2 | 3;
  }
}
