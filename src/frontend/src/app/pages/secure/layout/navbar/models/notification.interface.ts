import { DateTime } from "luxon";

export interface AppNotification {
    id: string;
    creationDate: DateTime;
    message: string;
    isRead: boolean;
}