import { Component, OnInit, OnDestroy, Input, EventEmitter, Output } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { EnvironmentService, ServerService } from '../../services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { take } from 'rxjs/operators'
import { Environment, Server } from '../../models';

@Component({
    selector: 'app-environment-editor',
    templateUrl: './environment-editor.component.html',
    styleUrls: ['./environment-editor.component.scss']
})
export class EnvironmentEditorComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    @Input() public environmentId: string;
    @Output() public canceled = new EventEmitter<boolean>();
    @Output() public saved = new EventEmitter<Environment>();
    public environment = new Environment();
    public servers: Array<Server>;
    private subscriptions = new Array<Subscription>();

    constructor(private route: ActivatedRoute,
        private router: Router,
        private environmentService: EnvironmentService,
        private serverService: ServerService) {
        this.subscriptions.push(this.environmentService.currentEnvironment$.subscribe(net => this.environment = net));
        this.subscriptions.push(this.serverService.getAll(false).subscribe(servers => this.servers = servers));
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public save() {
        this.saved.next(this.environment);
    }

    public cancel() {
        this.canceled.next(true);
    }
}
