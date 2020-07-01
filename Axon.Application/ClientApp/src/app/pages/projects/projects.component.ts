import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectService, AuthenticationService, SearchService } from '@app/services';
import { BehaviorSubject } from 'rxjs';
import { Project } from '@app/models';
import { map } from 'rxjs/operators';
import { fadeInOnEnterAnimation, fadeOutOnLeaveAnimation } from 'angular-animations';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss'],
  animations: [
    fadeInOnEnterAnimation({ anchor: 'enter', duration: 1000, delay: 100 }),
    fadeOutOnLeaveAnimation({ anchor: 'leave', duration: 500 })
  ]
})
export class ProjectsComponent implements OnInit, OnDestroy {
  public isCollapsed = true;
  public projects$ = new BehaviorSubject<Array<Project>>([]);
  public edition = true;
  public mode: string = 'list';
  public filteredData$ = new BehaviorSubject<Array<Project>>([]);

  constructor(private route: ActivatedRoute,
    private router: Router,
    private search: SearchService,
    private authService: AuthenticationService,
    private projectService: ProjectService) { 
      this.search.searchTerm$.subscribe(value => {
        this.projectService.projects$.pipe(
          map(projects => {
            return projects.filter(project => {
              return value.length === 0 || project.name.toLowerCase().indexOf(value) > -1;
            });
          })
        ).subscribe(res => this.filteredData$.next(res));
      });
      this.projectService.projects$.subscribe(projects => {
        this.search.searchTerm$.pipe(
          map(value => {
            return projects.filter(project => {
              return value.length === 0 || project.name.toLowerCase().indexOf(value) > -1;
            });
          })
        ).subscribe(res => this.filteredData$.next(res));
      });
      this.projects$ = this.projectService.projects$;
      this.projectService.getAll(true).subscribe();
      this.authService.getAll(false).subscribe();
    }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const id = params['id'];
      if(!!id) {
        this.show(id);
      }
    });
  }

  ngOnDestroy() {
  }

  public create() {
    this.projectService.currentProject$.next(new Project());
    this.mode = 'creation';
  }

  public show(id: string) {
    this.projectService.get(id).subscribe(res => this.mode = 'edition');
  }

  public cancelEdition(event: boolean) {
    if(event) {
      this.projectService.currentProject$.next(null);
      this.mode = 'list';
    }
  }
}
