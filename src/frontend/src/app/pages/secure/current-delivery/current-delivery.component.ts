import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CurrentDeliveryService } from './services/current-delivery.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { tap } from 'rxjs';
import { CurrentDeliveryGetSingleResponse } from './interfaces/current-delivery-get-single-response';
import { AuthService } from 'src/app/services/auth.service';
import { AvailableDeliveriesService } from '../available-deliveries/services/available-deliveries.service';

@Component({
  selector: 'app-current-delivery',
  templateUrl: './current-delivery.component.html',
  styleUrls: ['./current-delivery.component.scss'],
  providers: [
    ConfirmationService,
    CurrentDeliveryService,
    AvailableDeliveriesService
  ]
})
export class CurrentDeliveryComponent implements OnInit {
  public delivery: CurrentDeliveryGetSingleResponse | undefined;
  public hasActiveDelivery = false;

  public warehouseLocation: google.maps.LatLngLiteral = {
    lat: 50.1425722,
    lng: 20.8328481
  };

  public orderLocation: google.maps.LatLngLiteral = {
    lat: 50.1425722,
    lng: 20.8328481
  };

  public warehouseMarker: google.maps.MarkerOptions = {
    draggable: false,
    position: this.warehouseLocation,
    title: `Warehouse location`,
    icon: {
      url: `assets/img/warehouse.png`,
      scaledSize: {
        width: 35,
        height: 35,
        equals: (_: google.maps.Size) => true
      }
    }
  }

  public orderMarker: google.maps.MarkerOptions = {
    draggable: false,
    position: this.orderLocation,
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
    private readonly msg: MessageService,
    private readonly availableDeliveriesService: AvailableDeliveriesService
  ) { }

  public ngOnInit(): void {
    this.fetch().subscribe();
    this.availableDeliveriesService.hasActiveDelivery$.subscribe(result => this.hasActiveDelivery = result);
  }

  public onPickupClick(event: Event) {
    this.service.pickup(this.delivery!.id).subscribe(() => {
      this.fetch().subscribe(() => {
        this.msg.add({ severity: 'success', summary: 'Success', detail: 'Delivery picked up!' });
      });
    });
  }

  // public onMarkAsLostClick(event: Event) {
  //   this.confirmationService.confirm({
  //     target: event.target!,
  //     message: 'Are you sure that you want mark this delivery as lost?',
  //     icon: 'pi pi-exclamation-triangle',
  //     accept: () => {
  //       this.setLost();
  //     },
  //     reject: () => { }
  //   });
  // }

  private fetch() {
    return this.service.getSingle().pipe(tap(delivery => {
      this.delivery = delivery;

      this.warehouseLocation.lat = delivery.warehouseLocation.gpsLatitude;
      this.warehouseLocation.lng = delivery.warehouseLocation.gpsLongitude;

      this.orderLocation.lat = delivery.orderLocation.gpsLatitude;
      this.orderLocation.lng = delivery.orderLocation.gpsLongitude;
    }));
  }
}