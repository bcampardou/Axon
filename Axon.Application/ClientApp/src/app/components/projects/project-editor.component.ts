import { Component, OnInit, OnDestroy, Input, EventEmitter, Output, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectService, EnvironmentService } from '@app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Project, Environment } from '@app/models';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-project-editor',
    templateUrl: './project-editor.component.html',
    styleUrls: ['./project-editor.component.scss']
})
export class ProjectEditorComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    @Input() public projectId: string;
    @Output() public canceled = new EventEmitter<boolean>();
    public project = new Project();
    private subscriptions = new Array<Subscription>();
    @ViewChild('content', {static: false}) content: any;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private toastr: ToastrService,
        private translateService: TranslateService,
        private modalService: NgbModal,
        private projectService: ProjectService,
        private environmentService: EnvironmentService) {
            this.subscriptions.push(this.projectService.currentProject$.subscribe(net => {
                this.project = net;
            }));
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public save() {
        this.projectService.post(this.project).subscribe(res => {
            this.project = res;
            this.toastr.success(this.translateService.instant('Successfully saved', 'Success'));
        });
    }

    public createEnvironment() {
        let env = new Environment();
        env.projectId = this.projectId;
        this.project.environments.push(env);
        this.environmentService.currentEnvironment$.next(env);
        let ref = this.modalService.open(this.content, { centered: true });
        ref.result.then(res => console.log(res));
    }

    public openEnvironment(environment: Environment) {
        this.environmentService.currentEnvironment$.next(environment);
        let ref = this.modalService.open(this.content, { centered: true });
    }

    public cancel() {
        this.canceled.next(true);
    }
}
