import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from './services/order.service';
import { OrdersGetSingleDeliveryResponse, OrdersGetSingleResponse } from './interfaces/orders-get-single-response';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ScreenService, ScreenSize } from 'src/app/services/screen.service';
import { catchError, throwError } from 'rxjs';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
  providers: [
    ConfirmationService,
    OrderService
  ]
})
export class OrderComponent implements OnInit {
  order: OrdersGetSingleResponse | undefined;

  public ScreenSize = ScreenSize;
  public isSettingDeliveredId = '0';

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
    public readonly screen: ScreenService,
    private readonly confirmationService: ConfirmationService,
    private readonly route: ActivatedRoute,
    private readonly orderService: OrderService,
    private readonly router: Router,
    private readonly msg: MessageService,
  ) { }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      const orderId = params['id'];
      this.fetch(orderId);
    });
  }

  public onMarkAsDeliveredClick(delivery: OrdersGetSingleDeliveryResponse) {
    this.isSettingDeliveredId = delivery.id;

    this.orderService.setDeliveryAsDelivered(this.order!.id, delivery.id)
      .pipe(
        catchError(err => {
          this.isSettingDeliveredId = '0';
          return throwError(() => err);
        })
      )
      .subscribe(() => {
        this.fetch(this.order!.id);

        setTimeout(() => {
          this.isSettingDeliveredId = '0';
          this.msg.add({ severity: 'success', summary: 'Success', detail: 'Delivery marked as delivered!' });
        }, 800);
      });
  }

  public confirm(event: Event) {
    this.confirmationService.confirm({
      target: event.target!,
      message: 'Are you sure that you want to cancel this order?',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.cancelOrder();
      },
      reject: () => { }
    });
  }

  public cancelOrder() {
    this.orderService.cancelOrder(this.order!.id).subscribe(() => {
      this.msg.add({ severity: 'success', summary: 'Success', detail: 'Order canceled!' });
      setTimeout(() => {
        this.router.navigateByUrl('secure/request-help');
      }, 1000);
    });
  }

  public getDelivererText(delivery: OrdersGetSingleDeliveryResponse): string {
    let result = `${delivery.delivererFullName} (${delivery.delivererEmail})`;

    if (delivery.delivererPhoneNumber) {
      result += `, ${delivery.delivererPhoneNumber}`;
    }

    return result;
  }

  private fetch(orderId: string) {
    this.orderService.getOrder(orderId).subscribe(order => {
      this.order = order;
      this.location.lat = order.gpsLatitude;
      this.location.lng = order.gpsLongitude;
    });
  }
}