import { Input, Component } from "@angular/core";
import { KnowledgeSheet } from "@app/models";
import { KnowledgeSheetService } from "@app/services";

@Component({
    selector: 'app-knowledge-sheet-viewer',
    templateUrl: './knowledge-sheet-viewer.component.html',
    styleUrls: ['./knowledge-sheet-viewer.component.scss']
  })
  export class KnowledgeSheetViewerComponent {

      @Input() public sheet: KnowledgeSheet;
      
      constructor(
          private ksService: KnowledgeSheetService,
      ) {}

      create() {
        
      }
  }