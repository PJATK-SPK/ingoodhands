import { HttpClient } from "@angular/common/http";
import { map, mergeMap, timer } from "rxjs";
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
                icon: 'pi pi-fw pi-user-plus',
                routerLink: '/secure/my-donations',
                alerts: timer(0, 60 * 1000)
                    .pipe(
                        mergeMap(() => httpClient.get<{ count: number }>(`${environment.api}/my-donations/not-delivered-count`)),
                        map(data => data.count)
                    )
            },
        ]
    },
];