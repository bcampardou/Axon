import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ServerService, AuthenticationService, SearchService, KnowledgeSheetService } from '@app/services';
import { BehaviorSubject } from 'rxjs';
import { Server, KnowledgeSheet } from '@app/models';
import { filter, map } from 'rxjs/operators';
import { fadeInOnEnterAnimation, fadeOutOnLeaveAnimation } from 'angular-animations';

@Component({
    selector: 'app-knowledge-sheet',
    templateUrl: './knowledge-sheet.component.html',
    styleUrls: ['./knowledge-sheet.component.scss'],
    animations: [
        fadeInOnEnterAnimation({ anchor: 'enter', duration: 1000, delay: 100 }),
        fadeOutOnLeaveAnimation({ anchor: 'leave', duration: 500 })
    ]
})
export class KnowledgeSheetComponent implements OnInit, OnDestroy {
    public isCollapsed = true;
    public sheet$ = new BehaviorSubject<KnowledgeSheet>(null);
    public mode: string = 'viewer';

    constructor(private route: ActivatedRoute,
        private router: Router,
        private search: SearchService,
        private authService: AuthenticationService,
        private knowledgeSheetService: KnowledgeSheetService) {
        this.sheet$ = this.knowledgeSheetService.currentKnowledgeSheet$;
        this.authService.getAll(false).subscribe();
    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            const id = params['id'];
            if (!!id) {
                this.show(id);
            }
        });
    }

    ngOnDestroy() {
    }

    public create() {
        const ks = new KnowledgeSheet();
        ks.parentId = this.sheet$.getValue().id;
        this.knowledgeSheetService.currentKnowledgeSheet$.next(ks);
        this.mode = 'editor';
    }

    public edit() {
        this.mode = 'editor';
    }

    public saved() {
        this.mode = 'viewer';
    }

    public show(id: string) {
        this.knowledgeSheetService.get(id).subscribe(res => console.log(res));
    }

    public cancelEdition(event: boolean) {
        if (event) {
            this.mode = 'viewer'
        }
    }
}
