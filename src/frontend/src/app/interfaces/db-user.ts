import { Role } from "../enums/role";

export interface DbUser {
    id: string;
    firstName: string;
    lastName?: string;
    email: string;
    phoneNumber?: string;
    warehouseId?: string;
    auth0Identifiers: string[];
    roles: Role[];
}