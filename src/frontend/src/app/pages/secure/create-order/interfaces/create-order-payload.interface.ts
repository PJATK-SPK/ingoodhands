import { CreateOrderProduct } from "./create-order-product.interface";

export interface CreateOrderPayload {
    addressId: string;
    products: CreateOrderProduct[];
}