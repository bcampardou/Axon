import { Component, OnInit, OnDestroy, Input, EventEmitter, Output, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NetworkService, AuthenticationService } from '@app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { take } from 'rxjs/operators'
import { Network, User } from '@app/models';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-network-editor',
    templateUrl: './network-editor.component.html',
    styleUrls: ['./network-editor.component.scss']
})
export class NetworkEditorComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    @Input() public networkId: string;
    @Input() public edition: boolean = true;
    @Output() public canceled = new EventEmitter<boolean>();
    public network = new Network();
    private subscriptions = new Array<Subscription>();
    @ViewChild('userContent', { static: false }) userContent: any;
    @ViewChild('userPickerContent', { static: false }) userPickerContent: any;
    @ViewChild('interventionModal', { static: false }) interventionModal: any;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private toastr: ToastrService,
        private translateService: TranslateService,
        private modalService: NgbModal,
        private authService: AuthenticationService,
        private networkService: NetworkService) {
        this.subscriptions.push(this.networkService.currentNetwork$.subscribe(net => this.network = net));
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public save() {
        this.networkService.post(this.network).subscribe(res => {
            this.network = res;
            this.toastr.success(this.translateService.instant('Successfully saved', 'Success'));
        });
    }

    public cancel() {
        this.canceled.next(true);
    }



    public addUser() {
        this.modalService.open(this.userPickerContent, { centered: true });
    }

    public openUser(userId: string) {
        this.authService.getUser(userId).subscribe(u => this.authService.currentUser$.next(u));
        let ref = this.modalService.open(this.userContent, { centered: true });
    }

    public pushUser(event: User, modal: any) {
        console.log(event);
        this.network.team.push(event);
        modal.dismiss('saved');
    }

    public openInterventionModal() {
        const ref = this.modalService.open(this.interventionModal, { centered: true, size: 'lg', backdrop: 'static' });
    }
}
