import { DonatePayloadProduct } from "./donate-payload-product";

export interface DonatePayload {
    warehouseId: number;
    products: DonatePayloadProduct[];
}