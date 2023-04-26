import { Role } from "src/app/enums/role";

export interface ManageUsersItem {
    id: string;
    fullName: string;
    warehouseName: string;
    email: string;
    pictureUrl: string;
    roles: Role[];
}