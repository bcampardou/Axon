import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ServerService } from '@app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Server } from '@app/models';
import { OperatingSystems } from '@app/models/operating-systems.model';

@Component({
    selector: 'app-server-list',
    templateUrl: './server-list.component.html',
    styleUrls: ['./server-list.component.scss']
})
export class ServerListComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    public page: number = 0;
    public pageSize: number = 10;
    public osOptions = OperatingSystems;
    @Input() public servers: Array<Server>;
    @Output() public serverSelected = new EventEmitter<string>();
    private subscriptions = new Array<Subscription>();

    constructor(
        private serverService: ServerService) {
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public select(server: Server) {
        this.serverSelected.next(server.id);
    }

    public getOSName(value: number) {
        return this.osOptions.find(os => os.value === value).name;
    }
}
