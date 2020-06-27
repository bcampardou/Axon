import { Component, OnInit, OnDestroy, Input, EventEmitter, Output, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectService, EnvironmentService, AuthenticationService } from '@app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Project, Environment, User } from '@app/models';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { SimplemdeComponent } from '../simplemde/simplemde.component';

@Component({
    selector: 'app-project-editor',
    templateUrl: './project-editor.component.html',
    styleUrls: ['./project-editor.component.scss']
})
export class ProjectEditorComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    @Input() public projectId: string;
    @Input() public edition: boolean = true;
    @Output() public canceled = new EventEmitter<boolean>();
    public project = new Project();
    private subscriptions = new Array<Subscription>();
    @ViewChild('description', { static: true }) private readonly description: SimplemdeComponent;

    @ViewChild('content', { static: false }) content: any;
    @ViewChild('userContent', { static: false }) userContent: any;
    @ViewChild('userPickerContent', { static: false }) userPickerContent: any;
    @ViewChild('interventionModal', { static: false }) interventionModal: any;

    constructor(private route: ActivatedRoute,
        private router: Router,
        private toastr: ToastrService,
        private translateService: TranslateService,
        private modalService: NgbModal,
        private projectService: ProjectService,
        private authService: AuthenticationService,
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

    public onSubmit() {
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

    public addUser() {
        this.modalService.open(this.userPickerContent, { centered: true });
    }

    public openUser(userId: string) {
        this.authService.getUser(userId).subscribe(u => this.authService.currentUser$.next(u));
        let ref = this.modalService.open(this.userContent, { centered: true });
    }

    public pushUser(event:User, modal: any) {
        console.log(event);
        this.project.team.push(event); 
        modal.dismiss('saved');
    }

    public openInterventionModal() {
        const ref = this.modalService.open(this.interventionModal, { centered: true, size: 'lg', backdrop: 'static' });
    }
}
