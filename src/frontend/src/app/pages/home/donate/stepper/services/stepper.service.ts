import { Injectable } from '@angular/core'
import { Subject } from 'rxjs';
import { Product } from '../interfaces/product';
import { Warehouse } from '../interfaces/warehouse';

const WAREHOUSE_KEY = 'donate-stepper-warehouse';
const PRODUCTS_KEY = 'donate-stepper-products';

@Injectable()
export class StepperService {
    public currentStep: 1 | 2 | 3 = 1;

    public onAddClick: Subject<null> = new Subject();

    public selectedWarehouse?: Warehouse;

    public products: Product[] = [{
        listItem: { name: '', unit: 'kg' },
        quantity: 10,
    }];

    public clear(): void {
        this.selectedWarehouse = undefined;
        this.products = [{
            listItem: { name: '', unit: 'kg' },
            quantity: 10,
        }];
    }

    public save(): void {
        const warehouse = JSON.stringify(this.selectedWarehouse);
        const products = JSON.stringify(this.products);

        localStorage.setItem(WAREHOUSE_KEY, warehouse);
        localStorage.setItem(PRODUCTS_KEY, products);
    }

    public load(): void {
        const warehouse = localStorage.getItem(WAREHOUSE_KEY);
        const products = localStorage.getItem(PRODUCTS_KEY);

        if (warehouse) {
            try {
                this.selectedWarehouse = JSON.parse(warehouse);
            } catch (e) { }
        }

        if (products) {
            try {
                this.products = JSON.parse(products);
            } catch (e) { }
        }
    }
}