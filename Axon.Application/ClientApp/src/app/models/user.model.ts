import { Tenant } from "./tenant.model";

export class User {
    public id:string;
    public userName: string;
    public email: string;
    public tenantId: string;
    public tenant: Tenant;
}