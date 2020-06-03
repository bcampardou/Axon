import { Component, OnInit, OnDestroy, Input, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectService } from '@app/services';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Project } from '@app/models';

@Component({
    selector: 'app-project-list',
    templateUrl: './project-list.component.html',
    styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    public page: number = 0;
    public pageSize: number = 10;
    @Input() public projects: Array<Project>;
    @Output() public projectSelected = new EventEmitter<string>();
    private subscriptions = new Array<Subscription>();

    constructor(
        private projectService: ProjectService) {
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.subscriptions.forEach(sub => sub.unsubscribe());
    }

    public select(project: Project) {
        this.projectSelected.next(project.id);
    }
}
