import { DateTime } from "luxon";

export interface MyDonationsRawItem {
    id: string;
    name: string;
    productsCount: number;
    creationDate: string;
    isDelivered: boolean;
}

export interface MyDonationsItem {
    id: string;
    name: string;
    productsCount: number;
    creationDate: DateTime;
    isDelivered: boolean;
}