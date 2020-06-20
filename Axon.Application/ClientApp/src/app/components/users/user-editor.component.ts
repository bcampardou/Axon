import { Component, OnInit, OnDestroy, Input, EventEmitter, Output } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NetworkService, AuthenticationService } from '@app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { take } from 'rxjs/operators'
import { User, Network, Tenant } from '@app/models';
import { OperatingSystems } from '@app/models/operating-systems.model';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { TenantService } from '@app/services/tenant.service';

@Component({
    selector: 'app-user-editor',
    templateUrl: './user-editor.component.html',
    styleUrls: ['./user-editor.component.scss']
})
export class UserEditorComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    @Input() public userId: string;
    @Input() public edition: boolean = true;
    @Output() public canceled = new EventEmitter<boolean>();
    public user = new User();
    public tenants: Array<Tenant>;
    private subscriptions = new Array<Subscription>();

    constructor(
        private toastr: ToastrService,
        private translateService: TranslateService,
        private authService: AuthenticationService,
        private tenantService: TenantService) {
            this.subscriptions.push(this.authService.currentUser$.subscribe(ser => this.user = ser));
            this.subscriptions.push(this.tenantService.getAll(false).subscribe(res => this.tenants = res));
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    setTenant(tenant: Tenant) {
        this.user.tenant = tenant;
        this.user.tenantId = tenant.id;
    }

    public onSubmit() {
        this.authService.post(this.user).subscribe(res => 
            {
                this.user = res;
                this.toastr.success(this.translateService.instant('Successfully saved', 'Success'));
            });
    }

    public cancel() {
        this.canceled.next(true);
    }
}
