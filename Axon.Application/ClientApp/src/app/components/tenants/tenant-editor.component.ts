import { Component, OnInit, OnDestroy, Input, EventEmitter, Output, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NetworkService, AuthenticationService } from '@app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { take } from 'rxjs/operators'
import { Tenant, Network, License } from '@app/models';
import { OperatingSystems } from '@app/models/operating-systems.model';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { TenantService } from '@app/services/tenant.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { LicenseService } from '@app/services/license.service';

@Component({
    selector: 'app-tenant-editor',
    templateUrl: './tenant-editor.component.html',
    styleUrls: ['./tenant-editor.component.scss']
})
export class TenantEditorComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    @Input() public tenantId: string;
    @Output() public canceled = new EventEmitter<boolean>();
    public currentLicense$: BehaviorSubject<License> = new BehaviorSubject<License>(null);
    public get currentLicense() {
        return this.currentLicense$.getValue();
    }
    public tenant = new Tenant();
    public osOptions = OperatingSystems;
    public networks: Array<Network>;
    private modal: NgbModalRef;
    private subscriptions = new Array<Subscription>();
    @ViewChild('content', {static: false}) content: any;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private toastr: ToastrService,
        private translateService: TranslateService,
        private tenantService: TenantService,
        private modalService: NgbModal,
        private networkService: NetworkService,
        private licenseService: LicenseService) {
            this.subscriptions.push(this.tenantService.currentTenant$.subscribe(ser => this.tenant = ser));
            this.subscriptions.push(this.networkService.getAll(false).subscribe(net => this.networks = net));
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public save() {
        this.tenantService.post(this.tenant).subscribe(res => 
            {
                this.tenant = res;
                this.toastr.success(this.translateService.instant('Successfully saved', 'Success'));
            });
    }

    public cancel() {
        this.canceled.next(true);
    }

    public openLicense(license: License) {
        this.licenseService.currentLicense$.next(license);
        this.modal = this.modalService.open(this.content, { centered: true });
    }

    public closeModal(type:string) {
        this.modal.dismiss(type); 
        this.licenseService.currentLicense$.next(null);
    }
}
