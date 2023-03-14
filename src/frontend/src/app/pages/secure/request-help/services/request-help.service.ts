import { Injectable } from "@angular/core";
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { Map } from "../interfaces/map.interface";

@Injectable()
export class RequestHelpService {

    constructor(private readonly http: HttpClient) { }

    getMapData(): Observable<Map> {
        return this.http.get<Map>(`${environment.api}/request-help/map`);
    }
}