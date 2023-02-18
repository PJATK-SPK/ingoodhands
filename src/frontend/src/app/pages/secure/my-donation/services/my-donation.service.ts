import { Injectable } from "@angular/core";
import { environment } from 'src/environments/environment';
import { Observable, map } from 'rxjs';
import { DateTime } from 'luxon';
import { HttpClient } from "@angular/common/http";
import { DonationDetails } from "../models/donation-details.interface";

@Injectable()
export class MyDonationService {

    constructor(private readonly http: HttpClient) { }

    getDonation(id: string): Observable<DonationDetails<DateTime>> {
        return this.http.get<DonationDetails<string>>(`${environment.api}/my-donations/${id}`).pipe(
            map(result => {
                return {
                    ...result,
                    creationDate: DateTime.fromISO(result.creationDate),
                    expireDate: DateTime.fromISO(result.expireDate),
                };
            }),
        );
    }
}