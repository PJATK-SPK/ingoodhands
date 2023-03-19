import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DeliveryService } from './services/delivery.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DeliveryGetSingleResponse } from './interfaces/delivery-get-single-response';

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

  constructor(
    private readonly confirmationService: ConfirmationService,
    private readonly route: ActivatedRoute,
    private readonly service: DeliveryService,
    private readonly router: Router,
    private readonly msg: MessageService
  ) { }

  public ngOnInit(): void {
    this.route.params.subscribe(params => {
      const deliveryId = params['id'];
      this.service.getSingle(deliveryId).subscribe(delivery => {
        this.delivery = delivery;
      });
    });
  }

  public onPickupClick(event: Event) {
    this.service.pickup(this.delivery!.id).subscribe(() => {
      this.service.getSingle(this.delivery!.id).subscribe(delivery => {
        this.msg.add({ severity: 'success', summary: 'Success', detail: 'Delivery picked up!' });
        this.delivery = delivery;
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
      this.service.getSingle(this.delivery!.id).subscribe(delivery => {
        this.msg.add({ severity: 'success', summary: 'Success', detail: 'Delivery set as lost!' });
        this.delivery = delivery;
      });
    });
  }
}