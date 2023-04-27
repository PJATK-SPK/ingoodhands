export interface AvailableDeliveriesListItem<DateType> {
    id: string;
    deliveryName: string;
    orderName: string;
    isDelivered: boolean;
    isLost: boolean;
    tripStarted: boolean;
    creationDate: DateType;
    productTypesCount: number;
}