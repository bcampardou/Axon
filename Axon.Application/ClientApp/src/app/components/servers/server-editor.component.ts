import { Component, OnInit, OnDestroy, Input, EventEmitter, Output } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ServerService, NetworkService } from 'src/app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { take } from 'rxjs/operators'
import { Server, Network } from 'src/app/models';
import { OperatingSystems } from 'src/app/models/operating-systems.model';

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
        this.serverService.post(this.server).subscribe(res => this.server = res);
    }

    public cancel() {
        this.canceled.next(true);
    }
}
