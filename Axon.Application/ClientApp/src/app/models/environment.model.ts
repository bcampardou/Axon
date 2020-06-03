import { Server } from "./server.model";


export class Environment {
    public name: string;
    public url: string;
    public serverId: string;
    public projectId: string;
    public server: Server;
}