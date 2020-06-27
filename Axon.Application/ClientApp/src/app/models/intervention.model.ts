import { User } from "./user.model";
import { IdentifiedModel } from "./identified.model";

export class Intervention extends IdentifiedModel {
    public description: string;
    public start: Date;
    public end: Date;
    public inChargeUserId:string;
    public inChargeUser: User;
    public dataId: string;
    public data: any;
    public type: 'network'|'server'|'project';
}