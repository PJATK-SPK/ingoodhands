import { Injectable } from "@angular/core";
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Address } from "src/app/interfaces/address";
import { CreateOrder } from "../interfaces/create-order.interface";
import { Product } from "../interfaces/product";

@Injectable()
export class CreateOrderService {

    constructor(private readonly http: HttpClient) { }

    public getProducts(): Observable<Product[]> {
        return this.http.get<Product[]>(`${environment.api}/create-order/products`);
    }

    public getAddresses(): Observable<Address[]> {
        return this.http.get<Address[]>(`${environment.api}/create-order/addresses`);
    }

    public deleteAddress(id: string): Observable<Address> {
        return this.http.get<Address>(`${environment.api}/create-order/addresses/${id}`);
    }

    public createOrder(payload: CreateOrder): Observable<CreateOrder> {
        return this.http.post<CreateOrder>(`${environment.api}/create-order`, payload);
    }
}