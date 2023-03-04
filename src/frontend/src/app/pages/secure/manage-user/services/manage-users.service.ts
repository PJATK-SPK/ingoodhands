import { Injectable } from "@angular/core";
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { ManageUserDetails } from "../models/manage-user-details";

@Injectable()
export class ManageUserDetailsService {

    constructor(private readonly http: HttpClient) { }

    public getUser(id: string): Observable<ManageUserDetails> {
        return this.http.get<ManageUserDetails>(`${environment.api}/manage-users/${id}`);
    }
}