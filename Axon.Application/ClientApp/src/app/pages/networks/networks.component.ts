import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NetworkService, AuthenticationService, SearchService } from '@app/services';
import { BehaviorSubject } from 'rxjs';
import { Network } from '@app/models';
import { map } from 'rxjs/operators';
import { fadeInOnEnterAnimation, fadeOutOnLeaveAnimation } from 'angular-animations';

@Component({
  selector: 'app-networks',
  templateUrl: './networks.component.html',
  styleUrls: ['./networks.component.scss'],
  animations: [
    fadeInOnEnterAnimation({ anchor: 'enter', duration: 1000, delay: 100 }),
    fadeOutOnLeaveAnimation({ anchor: 'leave', duration: 500 })
  ]
})
export class NetworksComponent implements OnInit, OnDestroy {
  public isCollapsed = true;
  public networks$ = new BehaviorSubject<Array<Network>>([]);
  public mode: string = 'list';
  public filteredData$ = new BehaviorSubject<Array<Network>>([]);

  constructor(private route: ActivatedRoute,
    private search: SearchService,
    private networkService: NetworkService,
    private authService: AuthenticationService) { 
      this.search.searchTerm$.subscribe(value => {
        this.networkService.networks$.pipe(
          map(networks => {
            return networks.filter(network => {
              return value.length === 0 || network.name.toLowerCase().indexOf(value) > -1;
            });
          })
        ).subscribe(res => this.filteredData$.next(res));
      });
      this.networkService.networks$.subscribe(networks => {
        this.search.searchTerm$.pipe(
          map(value => {
            return networks.filter(network => {
              return value.length === 0 || network.name.toLowerCase().indexOf(value) > -1;
            });
          })
        ).subscribe(res => this.filteredData$.next(res));
      });
      this.networks$ = this.networkService.networks$;
      this.networkService.getAll(true).subscribe();
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
    const network = new Network();
    network.tenantId = this.authService.tenant.id;
    this.networkService.currentNetwork$.next(network);
    this.mode = 'creation';
  }

  public show(id: string) {
    this.networkService.get(id).subscribe(res => this.mode = 'edition');
  }

  public cancelEdition(event: boolean) {
    if(event) {
      this.networkService.currentNetwork$.next(null);
      this.mode = 'list';
    }
  }
}
