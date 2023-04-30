export interface CurrentDeliveryGetSingleProductResponse {
    name: string;
    quantity: number;
    unit: string;
}

export interface CurrentDeliveryGetSingleLocationResponse {
    countryName: string;
    gpsLatitude: number;
    gpsLongitude: number;
    city: string;
    postalCode: string;
    fullStreet: string;
}

export interface CurrentDeliveryGetSingleResponse {
    id: string;
    deliveryName: string;
    orderName: string;
    warehouseName: string;
    isDelivered: boolean;
    isLost: boolean;
    tripStarted: boolean;
    delivererFullName: string;
    creationDate: string;
    needyFullName: string;
    needyPhoneNumber: string;
    needyEmail: string;
    warehouseLocation: CurrentDeliveryGetSingleLocationResponse;
    orderLocation: CurrentDeliveryGetSingleLocationResponse;
    products: CurrentDeliveryGetSingleProductResponse[];
}