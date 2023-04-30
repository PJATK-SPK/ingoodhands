export interface CurrentDeliveryGetSingleProductResponse {
    name: string;
    quantity: number;
    unit: string;
}

export interface CurrentDeliveryGetSingleResponse {
    id: string;
    deliveryName: string;
    orderName: string;
    isDelivered: boolean;
    isLost: boolean;
    tripStarted: boolean;
    delivererFullName: string;
    creationDate: string;
    countryName: string;
    gpsLatitude: number;
    gpsLongitude: number;
    city: string;
    postalCode: string;
    fullStreet: string;
    products: CurrentDeliveryGetSingleProductResponse[];
}