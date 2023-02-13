import { Component, OnInit } from '@angular/core';
import { MyDonationsService } from './services/my-donations.service';
import { PagedResult } from 'src/app/interfaces/paged-result';
import { MyDonationsItem } from './models/my-donations-item';
import { DateTime } from 'luxon';

@Component({
  selector: 'app-my-donations',
  templateUrl: './my-donations.component.html',
  styleUrls: ['./my-donations.component.scss'],
  providers: [
    MyDonationsService
  ]
})
export class MyDonationsComponent implements OnInit {

  public DateTime = DateTime;
  public page = 1;
  public pageSize = 10;
  public pagedResult: PagedResult<MyDonationsItem> | undefined;

  constructor(private readonly service: MyDonationsService) { }

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
    this.service.getDonations(this.page, this.pageSize).subscribe(result => {
      this.pagedResult = result;
    });
  }
}
