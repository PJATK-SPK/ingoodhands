import { Role } from "src/app/enums/role";

export interface ManageUserDetails {
    id: string;
    fullName: string;
    warehouseId: string;
    roles: Role[];
}