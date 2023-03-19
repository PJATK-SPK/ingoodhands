import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DeliveryGetSingleResponse } from '../interfaces/delivery-get-single-response';

@Injectable()
export class DeliveryService {

    constructor(private http: HttpClient) { }

    getSingle(id: string): Observable<DeliveryGetSingleResponse> {
        return this.http.get<DeliveryGetSingleResponse>(`${environment.api}/deliveries/${id}`);
    }

    setLost(id: string): Observable<void> {
        return this.http.post<void>(`${environment.api}/deliveries/${id}/set-lost`, {});
    }

    pickup(id: string): Observable<void> {
        return this.http.post<void>(`${environment.api}/deliveries/${id}/pickup`, {});
    }
}