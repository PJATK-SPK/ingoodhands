import { Role } from "src/app/enums/role";

export interface ManageUserDetails {
    id: string;
    fullName: string;
    email: string;
    warehouseId: string;
    roles: Role[];
}