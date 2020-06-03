import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NetworkService } from 'src/app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Network } from 'src/app/models';

@Component({
    selector: 'app-network-list',
    templateUrl: './network-list.component.html',
    styleUrls: ['./network-list.component.scss']
})
export class NetworkListComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    public page: number = 0;
    public pageSize: number = 10;
    @Input() public networks: Array<Network>;
    @Output() public networkSelected = new EventEmitter<string>();
    private subscriptions = new Array<Subscription>();

    constructor(
        private networkService: NetworkService) {
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public select(network: Network) {
        this.networkSelected.next(network.id);
    }
}
