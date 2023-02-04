import { Injectable } from '@angular/core'
import { Step1Service } from './step-1.service';
import { Step2Service } from './step-2.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { FormItem } from '../interfaces/form-item';
import { Warehouse } from '../interfaces/warehouse';

export const DONATE_FORM_DATA_KEY = 'donate-form-data';
export interface DonateFormData {
    items: FormItem[];
    warehouse: Warehouse;
}

@Injectable()
export class StepperService {
    constructor(
        public readonly step1: Step1Service,
        public readonly step2: Step2Service,
        private readonly router: Router,
        private readonly msg: MessageService,
    ) { }

    public currentIndex = 0;

    public loginAndGoToFinalConfirm() {
        this.saveDonateFormData();

        this.msg.add({ severity: 'info', summary: 'Redirection', detail: 'You will be redirected to final confirmation screen' });

        setTimeout(() => {
            this.router.navigateByUrl('/secure/confirm-donation');
        }, 2000);
    }

    private saveDonateFormData() {
        const data: DonateFormData = {
            items: this.step1.getData(),
            warehouse: this.step2.allWarehouses.find(x => x.id === this.step2.selectedWarehouseId)!,
        };

        localStorage.setItem(DONATE_FORM_DATA_KEY, JSON.stringify(data));
    }
}