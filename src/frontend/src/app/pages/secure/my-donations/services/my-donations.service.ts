import { Injectable } from "@angular/core";
import { PagedResult } from 'src/app/interfaces/paged-result';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs';
import { DateTime } from 'luxon';
import { MyDonationsRawItem } from "../models/my-donations-item";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class MyDonationsService {

    constructor(private readonly http: HttpClient) { }

    getDonations(page: number, pageSize: number) {
        return this.http.get<PagedResult<MyDonationsRawItem>>(environment.api + '/my-donations', {
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