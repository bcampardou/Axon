import { IdentifiedModel } from "./identified.model";
import { Environment } from "./environment.model";


export class Project extends IdentifiedModel {
    public name: string;
    public environments: Array<Environment> = [];
}