import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NetworkService } from 'src/app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { take } from 'rxjs/operators'
import { Network } from 'src/app/models';

@Component({
    selector: 'app-network-editor',
    templateUrl: './network-editor.component.html',
    styleUrls: ['./network-editor.component.scss']
})
export class NetworkEditorComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    @Input() public networkId: string;
    public network = new Network();
    private subscriptions = new Array<Subscription>();

    constructor(private route: ActivatedRoute,
        private router: Router,
        private networkService: NetworkService) {
            this.subscriptions.push(this.networkService.currentNetwork$.subscribe(net => this.network = net));
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public save() {
        this.networkService.post(this.network).subscribe(res => this.network = res);
    }
}
