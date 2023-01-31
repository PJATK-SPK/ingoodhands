import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Warehouse } from '../../interfaces/warehouse';
import { StepperService } from '../../services/stepper.service';

@Component({
  selector: 'app-donate-step-2',
  templateUrl: './donate-step-2.component.html',
  styleUrls: ['./donate-step-2.component.scss']
})
export class DonateStep2Component implements OnInit {

  constructor(
    private readonly service: StepperService,
    private readonly httpClient: HttpClient,
  ) { }

  public allWarehouses: Warehouse[] = [];
  private _selectedWarehouseId?: number;

  public get selectedWarehouseId(): number | undefined {
    return this._selectedWarehouseId;
  }

  public set selectedWarehouseId(v: number | undefined) {
    this._selectedWarehouseId = v;
    this.service.selectedWarehouse = this.allWarehouses.find(w => w.id === v);
  }

  public ngOnInit(): void {
    this.fetchWarehouses().subscribe(() => {
      if (this.service.selectedWarehouse) {
        this.selectedWarehouseId = this.service.selectedWarehouse.id;
      }
    });
  }

  private fetchWarehouses(): Observable<Warehouse[]> {
    return this.httpClient.get<Warehouse[]>(`${environment.api}/donate/warehouses`).pipe(
      tap(data => {
        this.allWarehouses = data;
      })
    );
  }
}
