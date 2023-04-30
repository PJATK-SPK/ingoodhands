import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DeliveryService } from './services/delivery.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DeliveryGetSingleResponse } from './interfaces/delivery-get-single-response';
import { catchError, tap, throwError } from 'rxjs';
import { FORCE_REFRESH_SIDEBAR } from '../layout/sidebar/sidebar.component';

@Component({
  selector: 'app-delivery',
  templateUrl: './delivery.component.html',
  styleUrls: ['./delivery.component.scss'],
  providers: [
    ConfirmationService,
    DeliveryService
  ]
})
export class DeliveryComponent implements OnInit {
  delivery: DeliveryGetSingleResponse | undefined;
  public isButtonOnAction = false;

  public location: google.maps.LatLngLiteral = {
    lat: 50.1425722,
    lng: 20.8328481
  };

  public marker: google.maps.MarkerOptions = {
    draggable: false,
    position: this.location,
    title: `Order location`,
    icon: {
      url: `assets/img/order.png`,
      scaledSize: {
        width: 35,
        height: 35,
        equals: (_: google.maps.Size) => true
      }
    }
  }

  constructor(
    private readonly confirmationService: ConfirmationService,
    private readonly route: ActivatedRoute,
    private readonly service: DeliveryService,
    private readonly msg: MessageService
  ) { }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      const deliveryId = params['id'];
      this.fetch(deliveryId).subscribe();
    });
  }


  public onMarkAsLostClick(event: Event) {
    this.confirmationService.confirm({
      target: event.target!,
      message: 'Are you sure that you want mark this delivery as lost?',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.setLost();
      },
      reject: () => { }
    });
  }

  public onStartTripClick(event: Event) {
    this.isButtonOnAction = true;

    this.service.pickup(this.delivery!.id)
      .pipe(
        catchError(err => {
          this.isButtonOnAction = false;
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.fetch(this.delivery!.id).subscribe(() => {
          setTimeout(() => {
            FORCE_REFRESH_SIDEBAR.next();
            this.msg.add({ severity: 'success', summary: 'Success', detail: 'Delivery picked up!' });
          }, 800);
        });
      });
  }

  public setLost() {
    this.service.setLost(this.delivery!.id)
      .pipe(
        catchError(err => {
          this.isButtonOnAction = false;
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.fetch(this.delivery!.id).subscribe(() => {
          setTimeout(() => {
            FORCE_REFRESH_SIDEBAR.next();
            this.msg.add({ severity: 'success', summary: 'Success', detail: 'Delivery set as lost!' });
          }, 800);
        });
      });
  }

  private fetch(deliveryId: string) {
    return this.service.getSingle(deliveryId).pipe(tap(delivery => {
      this.delivery = delivery;
      this.location.lat = delivery.gpsLatitude;
      this.location.lng = delivery.gpsLongitude;
    }));
  }
}