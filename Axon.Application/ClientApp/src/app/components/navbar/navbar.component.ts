import { Component, OnInit, ElementRef, OnDestroy } from '@angular/core';
import { Location, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { Router } from '@angular/router';
import { SearchService, AuthenticationService } from '@app/services';
import { User } from '@app/models';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, OnDestroy {
  public focus;
  public location: Location;
  public currentUser: User;
  public _subscriptions = new Array<Subscription>();

  constructor(location: Location,
    public search: SearchService,
    private element: ElementRef,
    private authService: AuthenticationService,
    private router: Router) {
    this.location = location;
    this._subscriptions.push(this.authService.authenticatedUser$.subscribe(user => this.currentUser = user));
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    this._subscriptions.forEach(sub => sub.unsubscribe());
  }

  getTitle() {
    var titlee = this.location.prepareExternalUrl(this.location.path());
    if (titlee.charAt(0) === '#') {
      titlee = titlee.slice(1);
    }
    return 'Dashboard';
  }

  logout() {
    this.authService.logout().subscribe(res => this.router.navigate(['/login'], { replaceUrl: true }));
    
  }
}
