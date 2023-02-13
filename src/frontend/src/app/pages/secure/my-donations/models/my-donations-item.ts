import { DateTime } from "luxon";

export interface MyDonationsRawItem {
    name: string;
    productsCount: number;
    creationDate: string;
    isDelivered: boolean;
}

export interface MyDonationsItem {
    name: string;
    productsCount: number;
    creationDate: DateTime;
    isDelivered: boolean;
}