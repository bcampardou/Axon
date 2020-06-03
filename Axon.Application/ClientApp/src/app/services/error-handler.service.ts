import { ToastrService } from "ngx-toastr";
import { Injectable } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";

@Injectable()
export class ErrorHandler {
    constructor(private toastr: ToastrService, private translateService: TranslateService) { }
    public toastError(err: any) {
        if (err.status === 401) {
            this.toastr.error(this.translateService.instant('Please log in to do this action.'));
        } else {
            this.toastr.error(this.translateService.instant('Action failed. Please try again or contact an administrator if the problem persists.'));
        }
    }
}