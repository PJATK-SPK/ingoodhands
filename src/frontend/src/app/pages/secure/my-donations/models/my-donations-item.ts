export interface MyDonationsItem<DateType> {
    id: string;
    name: string;
    productsCount: number;
    creationDate: DateType;
    isDelivered: boolean;
    isExpired: boolean;
}