import { Injectable } from "@angular/core";
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Address } from "src/app/interfaces/address";
import { CreateOrderPayload } from "../interfaces/create-order-payload.interface";
import { Product } from "../interfaces/product";
import { ListAddress } from "../interfaces/list-address";

@Injectable()
export class CreateOrderService {

    constructor(private readonly http: HttpClient) { }

    public readonly products$ =
        this.http.get<Product[]>(`${environment.api}/create-order/products`);

    public readonly addresses$ =
        this.http.get<ListAddress[]>(`${environment.api}/create-order/addresses`);

    public deleteAddress(id: string): Observable<Address> {
        return this.http.delete<Address>(`${environment.api}/create-order/addresses/${id}`);
    }

    public createOrder(payload: CreateOrderPayload): Observable<CreateOrderPayload> {
        return this.http.post<CreateOrderPayload>(`${environment.api}/create-order`, payload);
    }
}