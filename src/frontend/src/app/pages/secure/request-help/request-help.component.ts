import { Component, OnInit } from '@angular/core';
import { RequestHelpService } from './services/request-help.service';
import { MapWarehouse } from './interfaces/map-warehouse.interface';
import { MapOrder } from './interfaces/map-order.interface';
import { Router } from '@angular/router';

interface Marker {
  options: google.maps.MarkerOptions;
  item: MapWarehouse | MapOrder;
}

@Component({
  selector: 'app-request-help',
  templateUrl: './request-help.component.html',
  styleUrls: ['./request-help.component.scss'],
  providers: [
    RequestHelpService
  ]
})
export class RequestHelpComponent implements OnInit {

  public location = {
    lat: 50.1425722,
    lng: 20.8328481
  };
  markers: Marker[] = [];

  constructor(
    private readonly service: RequestHelpService,
    private readonly router: Router) { }

  public ngOnInit(): void {
    this.service.getMapData().subscribe(data => {
      data.orders.map(item => this.markers.push(this.createOrderMarker(item)));
      data.warehouses.map(item => this.markers.push(this.createWarehouseMarker(item)));
    })
  }

  public onMarkerClick(marker: MapWarehouse | MapOrder, event: google.maps.MapMouseEvent) {
    const isOrder = marker.name.toUpperCase().includes('ORD');

    if (!isOrder) {
      return;
    }

    const order = marker as MapOrder;
    this.router.navigate(['secure/order', order.id]);
  }

  private createWarehouseMarker(warehouse: MapWarehouse): Marker {
    const options = {
      draggable: false,
      position: {
        lat: warehouse.gpsLatitude,
        lng: warehouse.gpsLongitude
      },
      title: `Warehouse ${warehouse.name}`,
      icon: {
        url: `assets/img/warehouse.png`,
        scaledSize: {
          width: 35,
          height: 35,
          equals: (_: google.maps.Size) => true
        }
      }
    };
    return { options, item: warehouse };
  }

  private createOrderMarker(order: MapOrder): Marker {
    const options = {
      draggable: false,
      position: {
        lat: order.gpsLatitude,
        lng: order.gpsLongitude
      },
      title: `Order ${order.name}`,
      icon: {
        url: `assets/img/order.png`,
        scaledSize: {
          width: 35,
          height: 35,
          equals: (_: google.maps.Size) => true
        }
      }
    };
    return { options, item: order };
  }

}
