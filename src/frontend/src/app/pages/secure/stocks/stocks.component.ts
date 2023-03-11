import { Component, OnInit } from '@angular/core';
import { StocksService } from './services/stocks.service';
import { StockItem } from './models/stock-item';
import { PagedResult } from 'src/app/interfaces/paged-result';
import { DateTime } from 'luxon';

@Component({
  selector: 'app-stocks-donation',
  templateUrl: './stocks.component.html',
  styleUrls: ['./stocks.component.scss'],
  providers: [
    StocksService,
  ]
})
export class StocksComponent implements OnInit {
  public DateTime = DateTime;
  public page = 1;
  public pageSize = 10;
  public pagedResult: PagedResult<StockItem> | undefined;

  constructor(public readonly service: StocksService) { }

  public getProductsText(count: number): string {
    return count > 1 ? `${count} products` : `${count} product`;
  }

  public ngOnInit(): void {
    this.fetch();
  }

  public paginate(event: { first: number, page: number, pageCount: number, rows: number }): void {
    this.page = event.page + 1;
    this.fetch();
  }

  private fetch(): void {
    this.service.getAll(this.page, this.pageSize).subscribe(result => {
      this.pagedResult = result;
    });
  }
}
