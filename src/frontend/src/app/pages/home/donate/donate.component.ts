import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-donate',
  templateUrl: './donate.component.html',
  styleUrls: ['./donate.component.scss']
})
export class DonateComponent {
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

  public onNextClick() {
    this.currentIndex++;
    if (this.currentIndex > this.steps.length - 1) {
      this.currentIndex = 0;
    }
  }
}
