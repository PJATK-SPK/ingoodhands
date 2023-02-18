import { HttpClient } from "@angular/common/http";
import { map, mergeMap, timer } from "rxjs";
import { Role } from "src/app/enums/role";
import { environment } from "src/environments/environment";

export const getSidebarConfig = (httpClient: HttpClient) => [
    {
        label: 'APPLICATION',
        pages: [
            {
                label: 'Start',
                icon: 'pi pi-fw pi-briefcase',
                routerLink: '/secure',
                alerts: undefined
            },
            {
                label: 'My donations',
                icon: 'pi pi-fw pi-file-import',
                routerLink: '/secure/my-donations',
                role: Role.donor,
                alerts: timer(0, 60 * 1000)
                    .pipe(
                        mergeMap(() => httpClient.get<{ count: number }>(`${environment.api}/my-donations/not-delivered-count`)),
                        map(data => data.count)
                    )
            },
            {
                label: 'Manage users',
                icon: 'pi pi-fw pi-user-edit',
                routerLink: '/secure/manage-users',
                role: Role.administrator,
            },
            {
                label: 'Pick up donation',
                icon: 'pi pi-fw pi-arrow-circle-up',
                routerLink: '/secure/pick-up-donation',
                role: Role.warehouseKeeper,
            },
            {
                label: 'Stocks',
                icon: 'pi pi-fw pi-list',
                routerLink: '/secure/stocks',
                role: Role.warehouseKeeper,
            },
        ]
    },
];