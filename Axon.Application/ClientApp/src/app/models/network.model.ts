import { Server } from "./server.model";
import { IdentifiedModel } from "./identified.model";


export class Network extends IdentifiedModel {
    public name: string;
    public description: string;
    public servers: Array<Server>;
}