import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { TenantService } from '@app/services/tenant.service';
import { Subscription } from 'rxjs';
import { Tenant } from '@app/models';

@Component({
    selector: 'app-tenant-list',
    templateUrl: './tenant-list.component.html',
    styleUrls: ['./tenant-list.component.scss']
})
export class TenantListComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    public page: number = 0;
    public pageSize: number = 10;
    @Input() public tenants: Array<Tenant>;
    @Output() public tenantSelected = new EventEmitter<string>();
    private subscriptions = new Array<Subscription>();

    constructor(
        private tenantService: TenantService) {
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public select(tenant: Tenant) {
        this.tenantSelected.next(tenant.id);
    }
}
