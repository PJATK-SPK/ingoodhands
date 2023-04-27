import { Component, OnInit } from '@angular/core';
import { PagedResult } from 'src/app/interfaces/paged-result';
import { DateTime } from 'luxon';
import { Subject, debounceTime, distinctUntilChanged, takeUntil } from 'rxjs';
import { Destroy } from 'src/app/services/destroy';
import { AvailableDeliveriesService } from './services/available-deliveries.service';
import { AvailableDeliveriesListItem } from './interfaces/available-deliveries-list-item';

@Component({
  selector: 'app-available-deliveries',
  templateUrl: './available-deliveries.component.html',
  styleUrls: ['./available-deliveries.component.scss'],
  providers: [
    AvailableDeliveriesService,
    Destroy
  ]
})
export class AvailableDeliveriesComponent implements OnInit {
  public DateTime = DateTime;
  public page = 1;
  public pageSize = 10;
  public pagedResult: PagedResult<AvailableDeliveriesListItem<DateTime>> | undefined;
  public onFilterChange = new Subject<Event>();
  public filter: string | undefined;

  constructor(
    public readonly service: AvailableDeliveriesService,
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
