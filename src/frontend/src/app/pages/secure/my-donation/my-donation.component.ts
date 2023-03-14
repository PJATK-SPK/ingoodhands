import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DonationDetails } from './models/donation-details.interface';
import { MyDonationService } from './services/my-donation.service';
import { DateTime } from 'luxon';

@Component({
  selector: 'app-my-donation',
  templateUrl: './my-donation.component.html',
  styleUrls: ['./my-donation.component.scss'],
  providers: [MyDonationService]
})
export class MyDonationComponent implements OnInit {
  public DateTime = DateTime;
  public id!: string;
  public donation!: DonationDetails<DateTime>;
  public location = {
    lat: 0,
    lng: 0
  };

  constructor(
    private readonly route: ActivatedRoute,
    private readonly service: MyDonationService,
  ) { }

  public ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id')!;
    this.service.getDonation(this.id).subscribe(donation => {
      this.location.lat = donation.warehouse.gpsLatitude;
      this.location.lng = donation.warehouse.gpsLongitude;
      this.donation = donation;
    });
  }
}
