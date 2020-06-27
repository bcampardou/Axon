import { OnInit, OnDestroy, Component, Input, EventEmitter, Output } from "@angular/core";
import { Intervention } from "@app/models/intervention.model";
import { NetworkService, ServerService, ProjectService } from "@app/services";
import { InterventionService } from "@app/services/intervention.service";
import { BehaviorSubject } from "rxjs";

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
    @Input() public dataId: string;
    @Input() public intervention: Intervention = new Intervention();
    @Input() public edition: boolean = true;
    @Output() public canceled = new EventEmitter<boolean>();
    @Output() public saved = new EventEmitter<Intervention>();

    constructor(private networkService: NetworkService,
        private serverService: ServerService,
        private interventionService: InterventionService,
        private projectService: ProjectService) {
            this.type$.subscribe(type => {
            switch (this.type) {
                case 'network': this.networkService.getAll(false).subscribe(nets => this.datas = nets);
                    break;
                case 'server': this.serverService.getAll(false).subscribe(servers => this.datas = servers);
                    break;
                case 'project': this.projectService.getAll(false).subscribe(projects => this.datas = projects);
                    break;
            }
        });
    }

    ngOnInit() {
        
    }

    ngOnDestroy() {

    }

    cancel() {
        this.canceled.next(true);
    }

    onSubmit() {
        this.interventionService.post(this.intervention);
    }
}