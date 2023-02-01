import { Component } from '@angular/core';
import { StepperService } from '../../services/stepper.service';

@Component({
  selector: 'app-donate-step-3',
  templateUrl: './donate-step-3.component.html',
  styleUrls: ['./donate-step-3.component.scss']
})
export class DonateStep3Component {
  constructor(public readonly service: StepperService,) { }
}
