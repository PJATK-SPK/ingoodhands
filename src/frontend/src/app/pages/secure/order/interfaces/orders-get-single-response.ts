export interface OrdersGetSingleDeliveryResponse {
    id: string;
    name: string;
    creationDate: string;
    isDelivered: boolean;
    tripStarted: boolean;
    isLost: boolean;
    delivererFullName: string;
    delivererEmail: string;
    delivererPhoneNumber: string;
    products: OrdersGetSingleProductResponse[];
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