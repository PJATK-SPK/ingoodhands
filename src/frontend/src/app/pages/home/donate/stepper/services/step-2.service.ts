import { Injectable } from '@angular/core'
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Warehouse } from '../interfaces/warehouse';

@Injectable()
export class Step2Service {
    public form = new FormGroup({
        warehouseId: new FormControl(null, [Validators.min(1), Validators.required]),
    });

    public allWarehouses: Warehouse[] = [];

    public get selectedWarehouseId(): string | null {
        return this.form.get('warehouseId')!.value;
    }

    public hasError(): string | undefined {
        const isValid = this.form.valid;
        if (!isValid) {
            return 'Please select warehouse!';
        }

        return undefined;
    }
}