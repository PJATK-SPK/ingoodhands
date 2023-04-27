import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CurrentDeliveryGetSingleResponse } from '../interfaces/current-delivery-get-single-response';

@Injectable()
export class CurrentDeliveryService {

    constructor(private http: HttpClient) { }

    getSingle(): Observable<CurrentDeliveryGetSingleResponse> {
        return this.http.get<CurrentDeliveryGetSingleResponse>(`${environment.api}/deliveries`);
    }

    setLost(id: string): Observable<void> {
        return this.http.post<void>(`${environment.api}/deliveries/${id}/set-lost`, {});
    }

    pickup(id: string): Observable<void> {
        return this.http.post<void>(`${environment.api}/deliveries/${id}/pickup`, {});
    }
}