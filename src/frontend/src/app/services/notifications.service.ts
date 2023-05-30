import { Injectable } from "@angular/core";
import { environment } from 'src/environments/environment';
import { Observable, Subject, map, tap } from 'rxjs';
import { HttpClient } from "@angular/common/http";
import { DateTime } from "luxon";
import { AppNotification } from "../pages/secure/layout/navbar/models/notification.interface";

const LOCAL_STORAGE_KEY = 'notifications';

@Injectable()
export class NotificationsService {
    public markAsReadClicked: Subject<void> = new Subject();
    public notifications: AppNotification[] = [];
    public get notificationsToRender() { return this.notifications.filter(c => !c.isRead).slice(0, 20); }
    public get notificationsCount() { return this.notifications.filter(c => !c.isRead).length; }
    constructor(private readonly httpClient: HttpClient) { }

    public init(): void {
        this.updateNotifications().subscribe();
    }

    clear(): void {
        localStorage.removeItem(LOCAL_STORAGE_KEY);
        this.notifications = [];
    }

    public updateNotifications(): Observable<AppNotification[]> {
        const readIds = this.getReadNotifications();
        return this.httpClient.get<{ id: string, creationDate: string, message: string }[]>(`${environment.api}/my-notifications`).pipe(
            map(notifications => notifications.map(c => ({ ...c, creationDate: DateTime.fromISO(c.creationDate), isRead: readIds.includes(c.id) })) as AppNotification[]),
            tap(notifications => this.notifications = notifications)
        );
    }

    public markAsRead(notification: AppNotification): void {
        const ids = this.getReadNotifications();
        ids.push(notification.id);
        notification.isRead = true;
        localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(ids));
        this.markAsReadClicked.next();
    }

    public getReadNotifications(): string[] {
        return JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY) ?? '[]') as string[];
    }
}