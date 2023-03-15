import { CreateOrderProduct } from "./create-order-product.interface";

export interface CreateOrder {
    addressId: string;
    products: CreateOrderProduct[];
}