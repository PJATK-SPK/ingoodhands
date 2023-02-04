import { DonatePayloadProduct } from "./donate-payload-product";

export interface DonatePayload {
    warehouseId: string;
    products: DonatePayloadProduct[];
}