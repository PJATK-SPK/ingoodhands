import { Component, OnDestroy, OnInit } from '@angular/core';
import { StepperService } from '../../services/stepper.service';
import { Observable, Subscription, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ProductListItem } from '../../interfaces/product-list-item';
import { Product } from '../../interfaces/product';

@Component({
  selector: 'app-donate-step-1',
  templateUrl: './donate-step-1.component.html',
  styleUrls: ['./donate-step-1.component.scss']
})
export class DonateStep1Component implements OnInit, OnDestroy {

  constructor(
    public readonly service: StepperService,
    private readonly httpClient: HttpClient,
  ) { }

  public sub = new Subscription();
  public allProducts: ProductListItem[] = [];
  public filteredProducts = this.allProducts;

  public unitOptions = [
    { label: 'kg', value: 'kg' },
    { label: 'l', value: 'l' },
    { label: 'pcs', value: 'pcs' },
  ];

  public ngOnInit(): void {
    this.fetchProducts().subscribe();

    this.sub.add(
      this.service.onAddClick.subscribe(() => {
        this.service.products.unshift({
          listItem: { name: '', unit: 'kg' },
          quantity: 10,
        });
      })
    );
  }

  public ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  public onRemoveClick(product: Product) {
    const index = this.service.products.indexOf(product);
    this.service.products.splice(index, 1);
  }

  public filterProduct(event: { originalEvent: PointerEvent, query: string }) {
    const filtered = [];

    const collectionToCheck = this.allProducts.filter(c =>
      !this.service.products.map(s => s.listItem.name).includes(c.name));

    for (let i = 0; i < collectionToCheck.length; i++) {
      let item = collectionToCheck[i];
      if (item.name.toLowerCase().indexOf(event.query.toLowerCase()) == 0) {
        filtered.push(item);
      }
    }

    this.filteredProducts = filtered;
  }

  private fetchProducts(): Observable<ProductListItem[]> {
    return this.httpClient.get<ProductListItem[]>(`${environment.api}/donate/products`).pipe(
      tap(data => {
        this.allProducts = data;
      })
    );
  }
}
