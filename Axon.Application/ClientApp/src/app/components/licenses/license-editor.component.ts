import { Component, OnInit, OnDestroy, Input, EventEmitter, Output } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { License } from '@app/models';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { LicenseService } from '@app/services/license.service';

@Component({
    selector: 'app-license-editor',
    templateUrl: './license-editor.component.html',
    styleUrls: ['./license-editor.component.scss']
})
export class LicenseEditorComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    public license: License;
    @Output() public canceled = new EventEmitter<boolean>();
    @Output() public saved = new EventEmitter<boolean>();
    private subscriptions = new Array<Subscription>();

    constructor(private licenseService: LicenseService) {
    }

    ngOnInit() {
        this.subscriptions.push(this.licenseService.currentLicense$.subscribe(l => this.license = l));
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public save() {
        this.saved.next(true);
    }

    public cancel() {
        this.canceled.next(true);
    }
}
