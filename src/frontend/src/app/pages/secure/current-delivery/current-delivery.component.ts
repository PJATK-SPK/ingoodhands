import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CurrentDeliveryService } from './services/current-delivery.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { tap } from 'rxjs';
import { CurrentDeliveryGetSingleResponse } from './interfaces/current-delivery-get-single-response';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-current-delivery',
  templateUrl: './current-delivery.component.html',
  styleUrls: ['./current-delivery.component.scss'],
  providers: [
    ConfirmationService,
    CurrentDeliveryService
  ]
})
export class CurrentDeliveryComponent implements OnInit {
  delivery: CurrentDeliveryGetSingleResponse | undefined;

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
    public readonly auth: AuthService,
    private readonly confirmationService: ConfirmationService,
    private readonly route: ActivatedRoute,
    private readonly service: CurrentDeliveryService,
    private readonly msg: MessageService
  ) { }

  public ngOnInit(): void {
    this.fetch().subscribe();
  }

  public onPickupClick(event: Event) {
    this.service.pickup(this.delivery!.id).subscribe(() => {
      this.fetch().subscribe(() => {
        this.msg.add({ severity: 'success', summary: 'Success', detail: 'Delivery picked up!' });
      });
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

  public setLost() {
    this.service.setLost(this.delivery!.id).subscribe(() => {
      this.fetch().subscribe(() => {
        this.msg.add({ severity: 'success', summary: 'Success', detail: 'Delivery set as lost!' });
      });
    });
  }

  private fetch() {
    return this.service.getSingle().pipe(tap(delivery => {
      this.delivery = delivery;
      this.location.lat = delivery.gpsLatitude;
      this.location.lng = delivery.gpsLongitude;
    }));
  }
}