import { Component, OnInit, Pipe, PipeTransform } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from './services/order.service';
import { OrdersGetSingleResponse } from './interfaces/orders-get-single-response';
import { DateTime } from 'luxon';



@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
  providers: [
    OrderService
  ]
})
export class OrderComponent implements OnInit {

  order: OrdersGetSingleResponse | undefined;
  public location = {
    lat: 50.1425722,
    lng: 20.8328481
  };

  constructor(private route: ActivatedRoute, private orderService: OrderService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const orderId = params['id'];
      this.orderService.getOrder(orderId).subscribe(order => {
        this.order = order;
        this.location.lat = order.gpsLatitude;
        this.location.lng = order.gpsLongitude;
      });
    });
  }
}