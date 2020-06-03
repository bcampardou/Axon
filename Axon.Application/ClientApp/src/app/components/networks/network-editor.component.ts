import { Component, OnInit, OnDestroy, Input, EventEmitter, Output } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NetworkService } from '@app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { take } from 'rxjs/operators'
import { Network } from '@app/models';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-network-editor',
    templateUrl: './network-editor.component.html',
    styleUrls: ['./network-editor.component.scss']
})
export class NetworkEditorComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    @Input() public networkId: string;
    @Output() public canceled = new EventEmitter<boolean>();
    public network = new Network();
    private subscriptions = new Array<Subscription>();

    constructor(private route: ActivatedRoute,
        private router: Router,
        private toastr: ToastrService,
        private translateService: TranslateService,
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
}
