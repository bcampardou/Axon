import { IdentifiedModel } from "./identified.model";

export class KnowledgeSheet extends IdentifiedModel {
    public name: string;
    public document: string;
    public parentId: string;
    public children: Array<KnowledgeSheet> = [];
    public isCollapsed = true;
}