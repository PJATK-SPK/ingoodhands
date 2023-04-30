import { Component, OnInit } from '@angular/core';
import { DeliveriesService } from './services/deliveries.service';
import { PagedResult } from 'src/app/interfaces/paged-result';
import { DateTime } from 'luxon';
import { DeliveriesListItem } from './interfaces/deliveries-list-item';
import { Subject, debounceTime, distinctUntilChanged, takeUntil } from 'rxjs';
import { Destroy } from 'src/app/services/destroy';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-deliveries',
  templateUrl: './deliveries.component.html',
  styleUrls: ['./deliveries.component.scss'],
  providers: [
    DeliveriesService,
    Destroy
  ]
})
export class DeliveriesComponent implements OnInit {
  public DateTime = DateTime;
  public page = 1;
  public pageSize = 10;
  public pagedResult: PagedResult<DeliveriesListItem<DateTime>> | undefined;
  public onFilterChange = new Subject<Event>();
  public filter: string | undefined;

  constructor(
    public readonly service: DeliveriesService,
    public readonly auth: AuthService,
    private readonly destroy$: Destroy) { }

  public getProductsText(count: number): string {
    return count > 1 ? `${count} products` : `${count} product`;
  }

  public ngOnInit(): void {
    this.fetch();

    this.onFilterChange
      .pipe(
        takeUntil(this.destroy$),
        debounceTime(700),
        distinctUntilChanged()
      ).subscribe(() => this.fetch());
  }

  public paginate(event: { first: number, page: number, pageCount: number, rows: number }): void {
    this.page = event.page + 1;
    this.fetch();
  }

  private fetch(): void {
    this.service.getList(this.page, this.pageSize, this.filter).subscribe(result => {
      this.pagedResult = result;
    });
  }
}
