import { Project } from "./project.model";
import { IdentifiedModel } from "./identified.model";
import { Network } from "./network.model";
import { User } from "./user.model";


export class Server extends IdentifiedModel {
    public name: string;
    public description: string;
    public businessDocumentationUrl: string;
    public technicalDocumentationUrl: string;
    public os: string;
    public version: string;
    public networkId: string;
    public projects: Array<Project> = new Array<Project>();
    public network: Network;
    public team: Array<User> = new Array<User>();
    public isCollapsed = false;
}