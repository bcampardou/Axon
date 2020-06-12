import { IdentifiedModel } from "./identified.model";
import { Environment } from "./environment.model";
import { User } from "./user.model";


export class Project extends IdentifiedModel {
    public name: string;
    public description: string;
    public businessDocumentationUrl: string;
    public technicalDocumentationUrl: string;
    public environments: Array<Environment> = [];
    public team: Array<User>;
}