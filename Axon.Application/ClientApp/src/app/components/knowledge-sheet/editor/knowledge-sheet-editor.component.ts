import { Input, Component, Output, EventEmitter } from "@angular/core";
import { KnowledgeSheet } from "@app/models";
import { KnowledgeSheetService } from "@app/services";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";

@Component({
    selector: 'app-knowledge-sheet-editor',
    templateUrl: './knowledge-sheet-editor.component.html',
    styleUrls: ['./knowledge-sheet-editor.component.scss']
})
export class KnowledgeSheetEditorComponent {

    @Input() public sheet: KnowledgeSheet;
    @Output() public saved = new EventEmitter<KnowledgeSheet>();
    @Output() public canceled = new EventEmitter<boolean>();

    constructor(private ksService: KnowledgeSheetService,
        private toastr: ToastrService,
        private translateService: TranslateService) { }

    cancel() {
        this.canceled.next(true);
    }
    onSubmit() {
        this.ksService.post(this.sheet).subscribe(res => {
            this.sheet = res;
            this.toastr.success(this.translateService.instant('Successfully saved', 'Success'));
            this.saved.next(this.sheet);
        });
    }
}