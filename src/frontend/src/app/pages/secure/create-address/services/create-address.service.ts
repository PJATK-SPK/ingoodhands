import { Injectable } from "@angular/core";
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Address } from "src/app/interfaces/address";

@Injectable()
export class CreateAddressService {

    constructor(private readonly http: HttpClient) { }

    public getCountries(): Observable<string[]> {
        return this.http.get<string[]>(`${environment.api}/create-order/countries`);
    }

    public addAddress(payload: Address): Observable<Address> {
        return this.http.post<Address>(`${environment.api}/create-order`, payload);
    }
}