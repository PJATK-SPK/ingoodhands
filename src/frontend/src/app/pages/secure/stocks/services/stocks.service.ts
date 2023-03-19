import { Injectable } from "@angular/core";
import { PagedResult } from 'src/app/interfaces/paged-result';
import { environment } from 'src/environments/environment';
import { Observable, map } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { StockItem } from "../models/stock-item";

@Injectable()
export class StocksService {

    constructor(private readonly http: HttpClient) { }

    public warehouseName$ = this.http.get<{ warehouseName: string }>(environment.api + '/stocks/warehouse-name')
        .pipe(map(c => c.warehouseName));

    public getAll(page: number, pageSize: number): Observable<PagedResult<StockItem>> {
        return this.http.get<PagedResult<StockItem>>(environment.api + '/stocks', {
            params: {
                page: page.toString(),
                pageSize: pageSize.toString()
            }
        });
    }
}