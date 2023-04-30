import { Component, OnInit } from '@angular/core';
import { PagedResult } from 'src/app/interfaces/paged-result';
import { DateTime } from 'luxon';
import { Subject, catchError, debounceTime, distinctUntilChanged, takeUntil, throwError } from 'rxjs';
import { Destroy } from 'src/app/services/destroy';
import { AvailableDeliveriesService } from './services/available-deliveries.service';
import { AvailableDeliveriesListItem } from './interfaces/available-deliveries-list-item';
import { AuthService } from 'src/app/services/auth.service';
import { ScreenService, ScreenSize } from 'src/app/services/screen.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { FORCE_REFRESH_SIDEBAR } from '../layout/sidebar/sidebar.component';

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
  public ScreenSize = ScreenSize;
  public page = 1;
  public pageSize = 10;
  public pagedResult: PagedResult<AvailableDeliveriesListItem<DateTime>> | undefined;
  public onFilterChange = new Subject<Event>();
  public filter: string | undefined;
  public hasActiveDelivery = false;
  public isAssigning = false;

  constructor(
    public readonly auth: AuthService,
    public readonly service: AvailableDeliveriesService,
    public readonly screen: ScreenService,
    public readonly router: Router,
    private readonly msg: MessageService,
    private readonly destroy$: Destroy) { }

  public getProductsText(count: number): string {
    return count > 1 ? `${count} products` : `${count} product`;
  }

  public ngOnInit(): void {
    this.fetch();

    this.service.hasActiveDelivery$.subscribe(result => this.hasActiveDelivery = result);

    this.onFilterChange
      .pipe(
        takeUntil(this.destroy$),
        debounceTime(700),
        distinctUntilChanged()
      ).subscribe(() => this.fetch());
  }

  public onAssignMeClick(item: AvailableDeliveriesListItem<DateTime>) {
    this.isAssigning = true;

    this.service.assignDelivery(item.id)
      .pipe(
        catchError(err => {
          this.isAssigning = false;
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        setTimeout(() => {
          setTimeout(() => {
            this.router.navigate(['/secure/current-delivery']);
            this.isAssigning = false;
          }, 1000);
          FORCE_REFRESH_SIDEBAR.next();
          this.msg.add({ severity: 'success', summary: 'Success', detail: 'Delivery assigned. Redirecting...' });
        }, 800);
      });
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
