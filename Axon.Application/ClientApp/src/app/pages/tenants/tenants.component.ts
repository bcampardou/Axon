import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { TenantService } from '@app/services/tenant.service';
import { BehaviorSubject } from 'rxjs';
import { Tenant } from '@app/models';
import { fadeInOnEnterAnimation, fadeOutOnLeaveAnimation } from 'angular-animations';

@Component({
  selector: 'app-tenants',
  templateUrl: './tenants.component.html',
  styleUrls: ['./tenants.component.scss'],
  animations: [
    fadeInOnEnterAnimation({ anchor: 'enter', duration: 1000, delay: 100 }),
    fadeOutOnLeaveAnimation({ anchor: 'leave', duration: 500 })
  ]
})
export class TenantsComponent implements OnInit, OnDestroy {
  public isCollapsed = true;
  public tenants$ = new BehaviorSubject<Array<Tenant>>([]);
  public mode: string = 'list';

  constructor(private router: Router,
    private tenantService: TenantService) { 
      this.tenants$ = this.tenantService.tenants$;
      this.tenantService.getAll(true).subscribe();
    }

  ngOnInit() {
  }

  ngOnDestroy() {
  }

  public create() {
    this.tenantService.currentTenant$.next(new Tenant());
    this.mode = 'creation';
  }

  public show(id: string) {
    this.tenantService.get(id).subscribe(res => this.mode = 'edition');
  }

  public cancelEdition(event: boolean) {
    if(event) {
      this.tenantService.currentTenant$.next(null);
      this.mode = 'list';
    }
  }
}
