export interface OrdersGetSingleDeliveryResponse {
    name: string;
    creationDate: string;
    isDelivered: boolean;
}

export interface OrdersGetSingleProductResponse {
    name: string;
    quantity: number;
    unit: string;
}

export interface OrdersGetSingleResponse {
    id: string;
    name: string;
    percentage: number;
    creationDate: string;
    countryName: string;
    gpsLatitude: number;
    gpsLongitude: number;
    city: string;
    postalCode: string;
    fullStreet: string;
    deliveries: OrdersGetSingleDeliveryResponse[];
    products: OrdersGetSingleProductResponse[];
}