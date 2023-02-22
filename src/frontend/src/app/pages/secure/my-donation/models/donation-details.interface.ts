import { DonationDetailsProduct } from "./donation-details-product.interface";
import { DonationDetailsWarehouse } from "./donation-details-warehouse.interface";

export interface DonationDetails<DateType> {
    id: string;
    name: string;
    creationDate: DateType;
    expireDate: DateType;
    isDelivered: boolean;
    isExpired: boolean;
    warehouse: DonationDetailsWarehouse;
    products: DonationDetailsProduct[];
}
