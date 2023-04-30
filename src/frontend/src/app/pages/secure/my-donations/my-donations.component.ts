import { Component, OnInit } from '@angular/core';
import { MyDonationsService } from './services/my-donations.service';
import { PagedResult } from 'src/app/interfaces/paged-result';
import { MyDonationsItem } from './models/my-donations-item';
import { DateTime } from 'luxon';
import { tap } from 'rxjs';

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
  public pagedResult: PagedResult<MyDonationsItem<DateTime>> | undefined;
  public score = 0;
  public loaded = false;
  public shouldCount = false;
  constructor(public readonly service: MyDonationsService) { }

  public getProductsText(count: number): string {
    return count > 1 ? `${count} products` : `${count} product`;
  }

  public ngOnInit(): void {
    this.fetch();
    this.loadScore().subscribe(() => this.loaded = true);
  }

  public paginate(event: { first: number, page: number, pageCount: number, rows: number }): void {
    this.page = event.page + 1;
    this.fetch();
  }

  private loadScore() {
    const key = 'MY_DONATIONS_SCORE';

    return this.service.score$.pipe(
      tap(score => {
        this.score = score;
        const latestRawScore = localStorage.getItem(key) || undefined;

        if (latestRawScore == undefined) {
          this.shouldCount = true;
          localStorage.setItem(key, score.toString());
        } else {
          const parsedLatestScore = parseInt(latestRawScore, 10);
          this.shouldCount = parsedLatestScore !== this.score;
        }
      })
    );
  }

  private fetch(): void {
    this.service.getDonations(this.page, this.pageSize).subscribe(result => {
      this.pagedResult = result;
    });
  }
}
