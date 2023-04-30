export interface AvailableDeliveriesListItem<DateType> {
    id: string;
    deliveryName: string;
    orderName: string;
    warehouseName: string
    warehouseCountryEnglishName: string
    warehouseFullStreet: string
    creationDate: DateType;
    productTypesCount: number;
}