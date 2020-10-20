import { Server } from "./server.model";
import { IdentifiedModel } from "./identified.model";
import { User } from "./user.model";


export class Network extends IdentifiedModel {
    public name: string;
    public description: string;
    public businessDocumentationUrl: string;
    public technicalDocumentationUrl: string;
    public servers: Array<Server> = new Array<Server>();
    public team: Array<User> = new Array<User>();
    public isCollapsed = true;
}