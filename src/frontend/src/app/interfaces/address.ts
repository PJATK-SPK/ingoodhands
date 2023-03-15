export interface Address {
    id: string;
    countryName: string;
    postalCode: string;
    city: string;
    street?: string;
    streetNumber?: string;
    apartment?: string;
    gpsLatitude: number;
    gpsLongitude: number;
}