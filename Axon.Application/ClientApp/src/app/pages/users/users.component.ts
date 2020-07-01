import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService, SearchService } from '@app/services';
import { BehaviorSubject } from 'rxjs';
import { User } from '@app/models';
import { map } from 'rxjs/operators';
import { fadeInOnEnterAnimation, fadeOutOnLeaveAnimation } from 'angular-animations';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  animations: [
    fadeInOnEnterAnimation({ anchor: 'enter', duration: 1000, delay: 100 }),
    fadeOutOnLeaveAnimation({ anchor: 'leave', duration: 500 })
  ]
})
export class UsersComponent implements OnInit, OnDestroy {
  public isCollapsed = true;
  public users$ = new BehaviorSubject<Array<User>>([]);
  public mode: string = 'list';
  public filteredData$ = new BehaviorSubject<Array<User>>([]);

  constructor(private router: Router,
    private search: SearchService,
    private route: ActivatedRoute,
    private authService: AuthenticationService) { 
      this.search.searchTerm$.subscribe(value => {
        this.authService.users$.pipe(
          map(users => {
            return users.filter(user => {
              return value.length === 0 || user.userName.toLowerCase().indexOf(value) > -1 || user.email.toLowerCase().indexOf(value) > -1;
            });
          })
        ).subscribe(res => this.filteredData$.next(res));
      });
      this.authService.users$.subscribe(users => {
        this.search.searchTerm$.pipe(
          map(value => {
            return users.filter(user => {
              return value.length === 0 || user.userName.toLowerCase().indexOf(value) > -1 || user.email.toLowerCase().indexOf(value) > -1;
            });
          })
        ).subscribe(res => this.filteredData$.next(res));
      });
      this.users$ = this.authService.users$;
      this.authService.getAll(true).subscribe();
    }

  ngOnInit() {
  }

  ngOnDestroy() {
  }

  public create() {
    this.authService.currentUser$.next(new User());
    this.mode = 'creation';
  }

  public show(id: string) {
    this.authService.get(id).subscribe(res => this.mode = 'edition');
  }

  public cancelEdition(event: boolean) {
    if(event) {
      this.authService.currentUser$.next(null);
      this.mode = 'list';
    }
  }
}
