import { Injectable } from "@angular/core";
import { environment } from 'src/environments/environment';
import { Observable, map, mergeMap, takeUntil, tap, timer } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { DateTime } from "luxon";
import { Destroy } from "src/app/services/destroy";
import { AppNotification } from "../models/notification.interface";

const LOCAL_STORAGE_KEY = 'notifications';

@Injectable()
export class NavbarNotificationService {

    private readonly notificationsTimer$ = timer(0, 15 * 1000).pipe(takeUntil(this.destroy$), mergeMap(() => this.updateNotifications()));

    public notifications: AppNotification[] = [];
    public get notificationsToRender() { return this.notifications.filter(c => !c.isRead).slice(0, 20); }
    public get notificationsCount() { return this.notifications.filter(c => !c.isRead).length; }

    constructor(
        private readonly httpClient: HttpClient,
        private readonly destroy$: Destroy) { }

    public init(): void {
        this.updateNotifications().subscribe();
        this.notificationsTimer$.subscribe();
    }

    private updateNotifications(): Observable<AppNotification[]> {
        const readIds = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY) || '[]') as string[];
        return this.httpClient.get<{ id: string, creationDate: string, message: string }[]>(`${environment.api}/my-notifications`).pipe(
            map(notifications => notifications.map(c => ({ ...c, creationDate: DateTime.fromISO(c.creationDate), isRead: readIds.includes(c.id) })) as AppNotification[]),
            tap(notifications => this.notifications = notifications)
        );
    }

    public markAsRead(notification: AppNotification): void {
        const ids = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY) || '[]') as string[];
        ids.push(notification.id);
        notification.isRead = true;
        localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(ids));
    }
}