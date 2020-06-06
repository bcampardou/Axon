import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '@app/services';
import { BehaviorSubject } from 'rxjs';
import { User } from '@app/models';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit, OnDestroy {
  public isCollapsed = true;
  public users$ = new BehaviorSubject<Array<User>>([]);
  public mode: string = 'list';

  constructor(private router: Router,
    private authService: AuthenticationService) { 
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
