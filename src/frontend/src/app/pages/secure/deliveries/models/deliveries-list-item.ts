export interface DeliveriesListItem<DateType> {
    id: string;
    deliveryName: string;
    orderName: string;
    isDelivered: boolean;
    isLost: boolean;
    tripStarted: boolean;
    delivererFullName: string;
    creationDate: DateType;
    productTypesCount: number;
}