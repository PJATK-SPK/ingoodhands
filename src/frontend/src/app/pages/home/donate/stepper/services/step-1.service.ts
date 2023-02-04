import { Injectable } from '@angular/core'
import { AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Product } from '../interfaces/product';
import { FormItem } from '../interfaces/form-item';

const PRODUCTS_KEY = 'donate-stepper-products';

@Injectable()
export class Step1Service {
    public form = new FormGroup({ items: new FormArray([this.createNewFormItemFormGroup()]) });

    public save(): void {
        const products = JSON.stringify(this.form.get('items')?.value);
        localStorage.setItem(PRODUCTS_KEY, products);
    }

    public load(): void {
        const products = localStorage.getItem(PRODUCTS_KEY);

        if (products) {
            try {
                const parsed = JSON.parse(products);
                const fa = this.form.get('items') as FormArray;
                fa.clear();
                parsed.forEach((p: any) => fa.push(this.createNewFormItemFormGroup(p.product, p.quantity)));
            } catch (e) { }
        }
    }

    public getData(): FormItem[] {
        const fa = this.form.get('items') as FormArray;
        return fa.controls.map(c => ({ product: c.get('product')?.value, quantity: c.get('quantity')?.value }));
    }

    public hasError(): string | undefined {
        const fa = this.form.get('items') as FormArray;

        const atLeast1 = fa.controls.length > 0;
        if (!atLeast1) {
            return 'At least one product is required';
        }

        const isValid = this.form.valid;
        if (!isValid) {
            if (fa.controls.some(c => c.get('product')?.errors?.['idNotZero'])) {
                return 'Please select a product';
            }

            return 'Please fill in all fields';
        }

        return undefined;
    }

    public onAddClick() {
        const fa = this.form.get('items') as FormArray;
        fa.push(this.createNewFormItemFormGroup());
    }

    public onRemoveClick(index: number) {
        const fa = this.form.get('items') as FormArray;
        fa.removeAt(index);
    }

    public onClearClick() {
        const fa = this.form.get('items') as FormArray;
        fa.clear();
        fa.push(this.createNewFormItemFormGroup());
    }

    private createNewFormItemFormGroup(product?: Product, quantity?: number): FormGroup {
        const result = new FormGroup({
            product: new FormControl(product ?? { id: 0, name: '', unit: 'kg' } as Product, [
                Validators.required,
                this.idNotZeroValidator,
            ]),
            quantity: new FormControl(quantity ?? 10, [Validators.min(1), Validators.max(100000), Validators.required]),
        });

        return result;
    }

    private idNotZeroValidator(control: AbstractControl): ValidationErrors | null {
        const value = control.value as Product;
        if (value.id === 0) {
            return { idNotZero: true };
        }

        return null;
    }
}