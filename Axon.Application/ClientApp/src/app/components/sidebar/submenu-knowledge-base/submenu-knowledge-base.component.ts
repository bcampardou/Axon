import { Input, Component } from "@angular/core";
import { KnowledgeSheet } from "@app/models";

@Component({
    selector: 'app-submenu-knowledge-base',
    templateUrl: './submenu-knowledge-base.component.html',
    styleUrls: ['./submenu-knowledge-base.component.scss']
  })
  export class SubmenuKnowledgeBaseComponent {

      @Input() public sheet: KnowledgeSheet;
      
      constructor() {}
  }