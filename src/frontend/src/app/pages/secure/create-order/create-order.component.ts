import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { CreateOrderService } from './services/create-address.service';
import { ListAddress } from './interfaces/list-address';
import { Product } from './interfaces/product';
import { catchError, throwError } from 'rxjs';
import { CreateOrderPayload } from './interfaces/create-order-payload.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.scss'],
  providers: [
    CreateOrderService
  ]
})
export class CreateOrderComponent implements OnInit {

  public form = new FormGroup({
    addressId: new FormControl(null, [Validators.required]),
    items: new FormArray([this.createNewFormItemFormGroup()]),
  });

  constructor(
    private readonly msg: MessageService,
    private readonly service: CreateOrderService,
    private readonly router: Router,
  ) { }

  public isSaving = false;
  public addresses: ListAddress[] = [];
  public allProducts: Product[] = [];
  public filteredProducts = this.allProducts;


  public get formItems() {
    return this.form.get('items') as FormArray;
  }

  public ngOnInit(): void {
    this.fetchAddresses();
    this.service.fetchProducts$.subscribe(data => this.allProducts = data);
  }

  public filterProduct(event: { originalEvent: PointerEvent, query: string }) {
    const filtered: Product[] = [];

    const fa = this.form.get('items') as FormArray;
    const fgs = fa.controls.map(c => (c as FormGroup));
    const selectedProducts = fgs.filter(c => c.get('product')?.value).map(c => c.get('product')!.value) as Product[];
    const names = selectedProducts.map(c => c.name);

    const collectionToCheck = this.allProducts.filter(c => !names.includes(c.name));

    collectionToCheck.forEach(item => {
      if (item.name.toLowerCase().indexOf(event.query.toLowerCase()) == 0) {
        filtered.push(item);
      }
    });

    this.filteredProducts = filtered;
  }

  public onAddClick() {
    const fa = this.form.get('items') as FormArray;
    fa.push(this.createNewFormItemFormGroup());
  }

  public onRemoveClick(index: number) {
    const fa = this.form.get('items') as FormArray;
    fa.removeAt(index);
  }

  public onSubmitClick(event: SubmitEvent): void {
    if (!this.form.valid) {

      if (this.form.controls.addressId.errors) {
        this.msg.add({ severity: 'error', summary: 'Error', detail: 'Please select an address.' });
        return;
      }

      const fa = this.form.get('items') as FormArray;
      const atLeast1 = fa.controls.length > 0;

      if (!atLeast1) {
        this.msg.add({ severity: 'error', summary: 'Error', detail: 'At least one product is required' });
        return;
      }

      if (fa.controls.some(c => c.get('product')?.errors?.['idNotZero'])) {
        this.msg.add({ severity: 'error', summary: 'Error', detail: 'Please select a product' });
        return;
      }

      this.msg.add({ severity: 'error', summary: 'Error', detail: 'Please fill out all required fields.' });

      return;
    }

    const payload = {
      addressId: this.form.controls.addressId.value!,
      products: this.form.controls.items.value.map((item) => ({
        id: item.product.id,
        quantity: item.quantity,
      })),
    } as CreateOrderPayload;

    this.isSaving = true;

    this.service.createOrder(payload).subscribe({
      next: (res) => {
        this.msg.add({ severity: 'success', summary: 'Success', detail: 'Order created!' });
        this.isSaving = false;
        setTimeout(() => {
          this.router.navigateByUrl('/secure/request-help');
        }, 1500);
      },
      error: () => {
        this.isSaving = false;
      }
    });
  }

  public onRemoveAddressClick(address: ListAddress): void {
    this.service.deleteAddress(address.id)
      .pipe(
        catchError(err => {
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.form.controls.addressId.setValue(null);
        this.fetchAddresses();
        this.msg.add({ severity: 'success', summary: 'Success', detail: 'Address has been removed.' });
      });
  }

  private fetchAddresses(): void {
    this.service.getAddresses().subscribe(res => {
      this.addresses = res;
    });
  }

  private createNewFormItemFormGroup(product?: Product, quantity?: number): FormGroup {
    const result = new FormGroup({
      product: new FormControl(product ?? { id: '', name: '', unit: 'kg' } as Product, [
        Validators.required,
        this.idNotZeroValidator,
      ]),
      quantity: new FormControl(quantity ?? 10, [Validators.min(1), Validators.max(100000), Validators.required]),
    });

    return result;
  }

  private idNotZeroValidator(control: AbstractControl): ValidationErrors | null {
    const value = control.value as Product;
    if (!value || value.id === '') {
      return { idNotZero: true };
    }

    return null;
  }

}
