import { Component, OnInit } from '@angular/core';
import { MyDonationsService } from './services/my-donations.service';
import { PagedResult } from 'src/app/interfaces/paged-result';
import { MyDonationsItem } from './models/my-donations-item';

@Component({
  selector: 'app-my-donations',
  templateUrl: './my-donations.component.html',
  styleUrls: ['./my-donations.component.scss'],
  providers: [
    MyDonationsService
  ]
})
export class MyDonationsComponent implements OnInit {

  public page = 1;
  public pageSize = 1;
  public pagedResult: PagedResult<MyDonationsItem> | undefined;

  constructor(private readonly service: MyDonationsService) { }

  ngOnInit(): void {
    this.fetch();
  }

  paginate(event: { first: number, page: number, pageCount: number, rows: number }): void {
    this.page = event.page + 1;
    this.fetch();
  }

  fetch(): void {
    this.service.getDonations(this.page, this.pageSize).subscribe(result => {
      this.pagedResult = result;
      console.log(result);
    });
  }
}
