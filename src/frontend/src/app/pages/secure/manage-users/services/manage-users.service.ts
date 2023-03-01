import { Injectable } from "@angular/core";
import { PagedResult } from 'src/app/interfaces/paged-result';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { ManageUsersItem } from "../models/manage-users-item";

@Injectable()
export class ManageUsersService {

    constructor(private readonly http: HttpClient) { }

    public getUsers(page: number, pageSize: number, filter?: string): Observable<PagedResult<ManageUsersItem>> {
        let params: { page: string, pageSize: string, filter?: string } = {
            page: page.toString(),
            pageSize: pageSize.toString(),
            filter
        };

        return this.http.get<PagedResult<ManageUsersItem>>(environment.api + '/manage-users', { params });
    }
}