import { Component, OnInit, OnDestroy, Input, EventEmitter, Output } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ServerService, NetworkService } from '@app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { take } from 'rxjs/operators'
import { Server, Network } from '@app/models';
import { OperatingSystems } from '@app/models/operating-systems.model';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-server-editor',
    templateUrl: './server-editor.component.html',
    styleUrls: ['./server-editor.component.scss']
})
export class ServerEditorComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    @Input() public serverId: string;
    @Output() public canceled = new EventEmitter<boolean>();
    public server = new Server();
    public osOptions = OperatingSystems;
    public networks: Array<Network>;
    private subscriptions = new Array<Subscription>();

    constructor(private route: ActivatedRoute,
        private router: Router,
        private toastr: ToastrService,
        private translateService: TranslateService,
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

    public save() {
        this.serverService.post(this.server).subscribe(res => 
            {
                this.server = res;
                this.toastr.success(this.translateService.instant('Successfully saved', 'Success'));
            });
    }

    public cancel() {
        this.canceled.next(true);
    }
}
