import { Server } from "./server.model";
import { IdentifiedModel } from "./identified.model";
import { User } from "./user.model";


export class Network extends IdentifiedModel {
    public name: string;
    public description: string;
    public businessDocumentationUrl: string;
    public technicalDocumentationUrl: string;
    public servers: Array<Server>;
    public tenantId: string;
    public team: Array<User>;
}