import { IdentifiedModel } from "./identified.model";
import { License } from "./license.model";

export class Tenant extends IdentifiedModel {
    public name: string;
    public licenses: Array<License> = [];
}