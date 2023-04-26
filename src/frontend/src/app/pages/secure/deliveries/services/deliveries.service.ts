import { Injectable } from "@angular/core";
import { PagedResult } from 'src/app/interfaces/paged-result';
import { environment } from 'src/environments/environment';
import { Observable, catchError, map } from 'rxjs';
import { DateTime } from 'luxon';
import { HttpClient } from "@angular/common/http";
import { DeliveriesListItem } from "../interfaces/deliveries-list-item";

@Injectable()
export class DeliveriesService {

    constructor(private readonly http: HttpClient) { }

    public warehouseName$ = this.http.get<{ warehouseName: string }>(environment.api + '/deliveries/warehouse-name')
        .pipe(
            map(c => c.warehouseName),
            catchError(() => '?')
        );

    public getList(page: number, pageSize: number, filter?: string): Observable<PagedResult<DeliveriesListItem<DateTime>>> {
        let params: { page: string, pageSize: string, filter?: string } = {
            page: page.toString(),
            pageSize: pageSize.toString(),
        };

        if (filter) {
            params.filter = filter;
        }

        return this.http.get<PagedResult<DeliveriesListItem<string>>>(environment.api + '/deliveries', { params })
            .pipe(
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