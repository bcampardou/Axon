import { Project } from "./project.model";
import { IdentifiedModel } from "./identified.model";
import { Network } from "./network.model";


export class Server extends IdentifiedModel {
    public name: string;
    public os: string;
    public version: string;
    public networkId: string;
    public projects: Array<Project>;
    public network: Network;
}