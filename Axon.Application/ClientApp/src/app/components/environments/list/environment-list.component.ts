import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { EnvironmentService } from '@app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Environment } from '@app/models';

@Component({
    selector: 'app-environment-list',
    templateUrl: './environment-list.component.html',
    styleUrls: ['./environment-list.component.scss']
})
export class EnvironmentListComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    public page: number = 0;
    public pageSize: number = 10;
    @Input() public environments: Array<Environment>;
    @Output() public environmentSelected = new EventEmitter<Environment>();
    private subscriptions = new Array<Subscription>();

    constructor(
        private environmentService: EnvironmentService) {
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public select(environment: Environment) {
        this.environmentSelected.next(environment);
    }
}
