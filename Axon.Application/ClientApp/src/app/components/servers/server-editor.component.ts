import { Component, OnInit, OnDestroy, Input, EventEmitter, Output, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ServerService, NetworkService, AuthenticationService } from '@app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { take } from 'rxjs/operators'
import { Server, Network, User } from '@app/models';
import { OperatingSystems } from '@app/models/operating-systems.model';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-server-editor',
    templateUrl: './server-editor.component.html',
    styleUrls: ['./server-editor.component.scss']
})
export class ServerEditorComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    @Input() public serverId: string;
    @Input() public edition: boolean = true;
    @Output() public canceled = new EventEmitter<boolean>();
    public server = new Server();
    public osOptions = OperatingSystems;
    public networks: Array<Network>;
    private subscriptions = new Array<Subscription>();
    @ViewChild('userContent', { static: false }) userContent: any;
    @ViewChild('userPickerContent', { static: false }) userPickerContent: any;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private toastr: ToastrService,
        private translateService: TranslateService,
        private modalService: NgbModal,
        private authService: AuthenticationService,
        private serverService: ServerService,
        private networkService: NetworkService) {
            this.subscriptions.push(this.serverService.currentServer$.subscribe(ser => this.server = ser));
            this.subscriptions.push(this.networkService.getAll(false).subscribe(net => this.networks = net));
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public onSubmit() {
        this.serverService.post(this.server).subscribe(res => 
            {
                this.server = res;
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
        this.server.team.push(event);
        modal.dismiss('saved');
    }
}
