import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProjectService } from '@app/services';
import { BehaviorSubject } from 'rxjs';
import { Project } from '@app/models';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.scss']
})
export class ProjectsComponent implements OnInit, OnDestroy {
  public isCollapsed = true;
  public projects$ = new BehaviorSubject<Array<Project>>([]);
  public edition = true;
  public mode: string = 'list';

  constructor(private route: ActivatedRoute,
    private router: Router,
    private projectService: ProjectService) { 
      this.projects$ = this.projectService.projects$;
      this.projectService.getAll(true).subscribe();
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
