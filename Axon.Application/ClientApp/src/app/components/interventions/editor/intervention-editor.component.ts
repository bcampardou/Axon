import { OnInit, OnDestroy, Component, Input, EventEmitter, Output } from "@angular/core";
import { Intervention } from "@app/models/intervention.model";
import { NetworkService, ServerService, ProjectService, AuthenticationService } from "@app/services";
import { InterventionService } from "@app/services/intervention.service";
import { BehaviorSubject, Subscription } from "rxjs";
import { User } from "@app/models";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";

@Component({
    selector: 'app-intervention-editor',
    templateUrl: './intervention-editor.component.html',
    styleUrls: ['./intervention-editor.component.scss']
})
export class InterventionEditorComponent implements OnInit, OnDestroy {
    public datas: Array<any> = [];
    private type$ = new BehaviorSubject<'server' | 'network' | 'project'>('project');
    @Input() public set type(value: 'server' | 'network' | 'project') {
        this.type$.next(value);
    }
    public get type() {
        return this.type$.getValue();
    }
    private dataId$ = new BehaviorSubject<string>(null);
    @Input() public set dataId(value: string) {
        this.dataId$.next(value);
    }
    public get dataId() {
        return this.dataId$.getValue();
    }
    @Input() public canChangeData = true;
    @Input() public intervention: Intervention = new Intervention();
    @Input() public edition: boolean = true;
    @Output() public canceled = new EventEmitter<boolean>();
    @Output() public saved = new EventEmitter<Intervention>();
    public users: Array<User> = [];

    private _subscriptions = new Array<Subscription>();

    constructor(private networkService: NetworkService,
        private serverService: ServerService,
        private interventionService: InterventionService,
        private authService: AuthenticationService,
        private toastr: ToastrService,
        private translateService: TranslateService,
        private projectService: ProjectService) {
        this._subscriptions.push(this.type$.subscribe(type => {
            this.intervention.type = type;
            switch (this.type) {
                case 'network': this.networkService.getAll(false).subscribe(nets => this.datas = nets);
                    break;
                case 'server': this.serverService.getAll(false).subscribe(servers => this.datas = servers);
                    break;
                case 'project': this.projectService.getAll(false).subscribe(projects => this.datas = projects);
                    break;
            }
        }));
        this._subscriptions.push(this.dataId$.subscribe(id => this.intervention.dataId = id));
        this._subscriptions.push(this.authService.getAll(false).subscribe(users => this.users = users));
    }

    ngOnInit() {
        console.log(this.intervention);
    }

    ngOnDestroy() {
        this._subscriptions.forEach(sub => sub.unsubscribe());
    }

    cancel() {
        this.canceled.next(true);
    }

    onSubmit() {
        this.interventionService.post(this.intervention).subscribe(res => {
            this.intervention = res;
            this.toastr.success(this.translateService.instant('Successfully saved', 'Success'));
            this.saved.next(this.intervention);
        });
    }

    setUser(user: User) {
        this.intervention.inChargeUser = user;
        this.intervention.inChargeUserId = user.id;
    }
}