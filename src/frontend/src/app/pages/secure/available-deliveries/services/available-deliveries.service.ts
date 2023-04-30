import { Injectable } from "@angular/core";
import { PagedResult } from 'src/app/interfaces/paged-result';
import { environment } from 'src/environments/environment';
import { Observable, map } from 'rxjs';
import { DateTime } from 'luxon';
import { HttpClient } from "@angular/common/http";
import { AvailableDeliveriesListItem } from "../interfaces/available-deliveries-list-item";

@Injectable()
export class AvailableDeliveriesService {

    constructor(private readonly http: HttpClient) { }

    public hasActiveDelivery$ = this.http.get<{ result: boolean }>(environment.api + '/available-deliveries/has-active-delivery')
        .pipe(
            map(c => c.result)
        );

    public assignDelivery(id: string) {
        return this.http.post(environment.api + `/available-deliveries/assign-delivery/${id}`, {});
    }

    public getList(page: number, pageSize: number, filter?: string): Observable<PagedResult<AvailableDeliveriesListItem<DateTime>>> {
        let params: { page: string, pageSize: string, filter?: string } = {
            page: page.toString(),
            pageSize: pageSize.toString(),
        };

        if (filter) {
            params.filter = filter;
        }

        return this.http.get<PagedResult<AvailableDeliveriesListItem<string>>>(environment.api + '/available-deliveries', { params })
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