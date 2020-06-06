import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { License } from '@app/models';

@Component({
    selector: 'app-license-list',
    templateUrl: './license-list.component.html',
    styleUrls: ['./license-list.component.scss']
})
export class LicenseListComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    public page: number = 0;
    public pageSize: number = 10;
    @Input() public licenses: Array<License>;
    @Output() public licenseSelected = new EventEmitter<License>();
    private subscriptions = new Array<Subscription>();

    constructor() {
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public select(license: License) {
        this.licenseSelected.next(license);
    }
}
