import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrdersGetSingleResponse } from '../interfaces/orders-get-single-response';
import { environment } from 'src/environments/environment';

@Injectable()
export class OrderService {

    constructor(private http: HttpClient) { }

    getOrder(id: string): Observable<OrdersGetSingleResponse> {
        return this.http.get<OrdersGetSingleResponse>(`${environment.api}/orders/${id}`);
    }
}