import { IdentifiedModel } from "./identified.model";

export class TableOfContentLine extends IdentifiedModel {
    public name: string;
    public children: Array<TableOfContentLine>;
}