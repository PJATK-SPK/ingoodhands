import { Injectable } from "@angular/core";
import { PagedResult } from 'src/app/interfaces/paged-result';
import { environment } from 'src/environments/environment';
import { Observable, map } from 'rxjs';
import { DateTime } from 'luxon';
import { HttpClient } from "@angular/common/http";
import { MyDonationsItem } from "../models/my-donations-item";

@Injectable()
export class MyDonationsService {

    constructor(private readonly http: HttpClient) { }

    getDonations(page: number, pageSize: number): Observable<PagedResult<MyDonationsItem<DateTime>>> {
        return this.http.get<PagedResult<MyDonationsItem<string>>>(environment.api + '/my-donations', {
            params: {
                page: page.toString(),
                pageSize: pageSize.toString()
            }
        }).pipe(
            map(result => {
                return {
                    ...result,
                    queryable: result.queryable.map(item => {
                        return {
                            ...item,
                            creationDate: DateTime.fromISO(item.creationDate)
                        }
                    })
                };
            }),
        );
    }
}