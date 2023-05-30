import { HttpClient } from "@angular/common/http";
import { Observable, map, mergeMap, timer } from "rxjs";
import { Role } from "src/app/enums/role";
import { AuthService } from "src/app/services/auth.service";
import { environment } from "src/environments/environment";

export interface SidebarPageConfig {
    label: string;
    icon: string;
    routerLink: string;
    alerts?: Observable<number>;
    role?: Role;
}

export interface SidebarCategoryConfig {
    role: Role | 'all';
    pages: SidebarPageConfig[];
}

export interface SidebarConfig {
    label: string;
    categories: SidebarCategoryConfig[];
}

export const getSidebarConfig = (httpClient: HttpClient, auth: AuthService) => [
    {
        label: auth.dbUser?.firstName ? auth.dbUser?.firstName + ' ' + auth.dbUser.lastName : 'APPLICATION',
        categories: [
            {
                role: 'all',
                pages: [
                    {
                        label: 'Start',
                        icon: 'pi pi-fw pi-briefcase',
                        routerLink: '/secure',
                        alerts: undefined,
                        role: undefined,
                    }
                ]
            },
            {
                role: Role.donor,
                pages: [
                    {
                        label: 'My donations',
                        icon: 'pi pi-fw pi-file-import',
                        routerLink: '/secure/my-donations',
                        role: Role.donor,
                        alerts: timer(0, 15 * 1000)
                            .pipe(
                                mergeMap(() => httpClient.get<{ count: number }>(`${environment.api}/my-donations/not-delivered-count`)),
                                map(data => data.count)
                            )
                    }
                ]
            },

            {
                role: Role.warehouseKeeper,
                pages: [
                    {
                        label: 'Pickup donation',
                        icon: 'pi pi-fw pi-arrow-circle-up',
                        routerLink: '/secure/pickup-donation',
                        role: Role.warehouseKeeper,
                    },
                    {
                        label: 'Warehouse stock',
                        icon: 'pi pi-fw pi-list',
                        routerLink: '/secure/stocks',
                        role: Role.warehouseKeeper,
                    },
                    {
                        label: 'Warehouse deliveries',
                        icon: 'pi pi-fw pi-box',
                        routerLink: '/secure/deliveries',
                        role: Role.warehouseKeeper,
                        alerts: timer(0, 15 * 1000)
                            .pipe(
                                mergeMap(() => httpClient.get<{ count: number }>(`${environment.api}/deliveries/warehouse-deliveries-count`)),
                                map(data => data.count)
                            )
                    }
                ]
            },
            {
                role: Role.needy,
                pages: [
                    {
                        label: 'Request help',
                        icon: 'pi pi-fw pi-map-marker',
                        routerLink: '/secure/request-help',
                        role: Role.needy,
                    },
                ]
            },
            {
                role: Role.deliverer,
                pages: [
                    {
                        label: 'Available deliveries',
                        icon: 'pi pi-fw pi-inbox',
                        routerLink: '/secure/available-deliveries',
                        role: Role.deliverer,
                        alerts: timer(0, 15 * 1000)
                            .pipe(
                                mergeMap(() => httpClient.get<{ count: number }>(`${environment.api}/available-deliveries/count`)),
                                map(data => data.count)
                            )
                    },
                    {
                        label: 'Current delivery',
                        icon: 'pi pi-fw pi-shopping-bag',
                        routerLink: '/secure/current-delivery',
                        role: Role.deliverer,
                    },
                ]
            },
            {
                role: Role.administrator,
                pages: [
                    {
                        label: 'Manage users',
                        icon: 'pi pi-fw pi-user-edit',
                        routerLink: '/secure/manage-users',
                        role: Role.administrator,
                    }
                ],
            },
        ]
    },
] as SidebarConfig[];