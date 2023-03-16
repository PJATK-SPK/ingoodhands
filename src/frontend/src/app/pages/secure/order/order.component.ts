import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderService } from './services/order.service';
import { OrdersGetSingleResponse } from './interfaces/orders-get-single-response';
import { ConfirmationService } from 'primeng/api';

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
  public location = {
    lat: 50.1425722,
    lng: 20.8328481
  };

  constructor(
    private readonly confirmationService: ConfirmationService,
    private readonly route: ActivatedRoute,
    private readonly orderService: OrderService,
    private readonly router: Router
  ) { }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      const orderId = params['id'];
      this.orderService.getOrder(orderId).subscribe(order => {
        this.order = order;
        this.location.lat = order.gpsLatitude;
        this.location.lng = order.gpsLongitude;
      });
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
      this.router.navigateByUrl('secure/request-help');
    });
  }
}