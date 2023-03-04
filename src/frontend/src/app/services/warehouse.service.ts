import { Injectable } from "@angular/core";
import { environment } from 'src/environments/environment';
import { Observable, of, tap } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Warehouse } from "../interfaces/warehouse";

@Injectable()
export class WarehouseService {
    constructor(private readonly http: HttpClient) { }

    private cache: Warehouse[] | undefined;

    public getAll(): Observable<Warehouse[]> {
        return this.cache
            ? of(this.cache)
            : this.http.get<Warehouse[]>(`${environment.api}/warehouses`).pipe(
                tap(warehouses => this.cache = warehouses),
            );
    }
}