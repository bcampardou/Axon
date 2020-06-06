import { IdentifiedModel } from "./identified.model";

export class License extends IdentifiedModel {
    public key: string;
    public startDate: Date;
    public endDate: Date;
    public isActive: boolean;
    public tenantId: string;
}