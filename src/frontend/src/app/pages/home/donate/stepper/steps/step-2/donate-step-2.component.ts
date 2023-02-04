import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Warehouse } from '../../interfaces/warehouse';
import { Step2Service } from '../../services/step-2.service';

@Component({
  selector: 'app-donate-step-2',
  templateUrl: './donate-step-2.component.html',
  styleUrls: ['./donate-step-2.component.scss']
})
export class DonateStep2Component implements OnInit {

  constructor(
    public readonly service: Step2Service,
    private readonly httpClient: HttpClient,
  ) { }

  private fetchWarehouses$ =
    this.httpClient.get<Warehouse[]>(`${environment.api}/donate-form/warehouses`).pipe(tap(data => this.service.allWarehouses = data));

  public ngOnInit(): void {
    this.fetchWarehouses$.subscribe();
  }
}
