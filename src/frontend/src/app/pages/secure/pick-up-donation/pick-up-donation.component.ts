import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-pick-up-donation',
  templateUrl: './pick-up-donation.component.html',
  styleUrls: ['./pick-up-donation.component.scss'],
  providers: [
  ]
})
export class PickUpDonationComponent implements OnInit {

  constructor(private readonly http: HttpClient) { }

  public ngOnInit(): void {
    this.getWarehouses().subscribe(result => console.log(result));
    this.pickUp('DNT123', 'PL001').subscribe(result => console.log(result));
  }

  // to service
  public getWarehouses(): Observable<any> {
    return this.http.get<any>(`${environment.api}/pick-up-donation/warehouses`);
  }

  // to service
  public pickUp(donationName: string, warehouseNumber: string): Observable<any> {
    return this.http.post<any>(`${environment.api}/pick-up-donation/${donationName}`, null, {
      params: {
        warehouseNumber: warehouseNumber
      }
    });
  }
}
