import { Component } from '@angular/core';
import { StepperService } from '../../services/stepper.service';
import { Warehouse } from '../../interfaces/warehouse';
import { FormItem } from '../../interfaces/form-item';

@Component({
  selector: 'app-donate-step-3',
  templateUrl: './donate-step-3.component.html',
  styleUrls: ['./donate-step-3.component.scss']
})
export class DonateStep3Component {
  constructor(public readonly service: StepperService) { }

  public get formItems(): FormItem[] {
    return this.service.step1.getData();
  }

  public get selectedWarehouse(): Warehouse {
    return this.service.step2.allWarehouses.find(x => x.id === this.service.step2.getSelectedWarehouseId())!;
  }
}
