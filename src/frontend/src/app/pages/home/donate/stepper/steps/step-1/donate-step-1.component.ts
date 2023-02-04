import { Component, OnInit } from '@angular/core';
import { tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Product } from '../../interfaces/product';
import { FormArray, FormGroup } from '@angular/forms';
import { Step1Service } from '../../services/step-1.service';

@Component({
  selector: 'app-donate-step-1',
  templateUrl: './donate-step-1.component.html',
  styleUrls: ['./donate-step-1.component.scss']
})
export class DonateStep1Component implements OnInit {

  constructor(
    public readonly service: Step1Service,
    private readonly httpClient: HttpClient,
  ) { }

  public allProducts: Product[] = [];
  public filteredProducts = this.allProducts;
  public unitOptions = [
    { label: 'kg', value: 'kg' },
    { label: 'l', value: 'l' },
    { label: 'pcs', value: 'pcs' },
  ];

  private readonly fetchProducts$ =
    this.httpClient.get<Product[]>(`${environment.api}/donate-form/products`).pipe(tap(data => this.allProducts = data));

  public ngOnInit(): void {
    this.fetchProducts$.subscribe();
    const items = this.service.load();
    this.service.set(items);
  }

  public get formItems() {
    return this.service.form.get('items') as FormArray;
  }

  public filterProduct(event: { originalEvent: PointerEvent, query: string }) {
    const filtered = [];

    const fa = this.service.form.get('items') as FormArray;
    const fgs = fa.controls.map(c => (c as FormGroup));
    const selectedProducts = fgs.map(c => c.get('product')!.value) as Product[];
    const names = selectedProducts.map(c => c.name);

    const collectionToCheck = this.allProducts.filter(c => !names.includes(c.name));

    for (let i = 0; i < collectionToCheck.length; i++) {
      let item = collectionToCheck[i];
      if (item.name.toLowerCase().indexOf(event.query.toLowerCase()) == 0) {
        filtered.push(item);
      }
    }

    this.filteredProducts = filtered;
  }
}
