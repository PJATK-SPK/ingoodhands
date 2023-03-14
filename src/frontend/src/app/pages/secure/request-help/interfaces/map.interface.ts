import { MapOrder } from "./map-order.interface";
import { MapWarehouse } from "./map-warehouse.interface";

export interface Map {
    warehouses: MapWarehouse[];
    orders: MapOrder[];
}