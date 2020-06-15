import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NetworkService, AuthenticationService } from '@app/services';
import { BehaviorSubject } from 'rxjs';
import { Network } from '@app/models';

@Component({
  selector: 'app-networks',
  templateUrl: './networks.component.html',
  styleUrls: ['./networks.component.scss']
})
export class NetworksComponent implements OnInit, OnDestroy {
  public isCollapsed = true;
  public networks$ = new BehaviorSubject<Array<Network>>([]);
  public mode: string = 'list';

  constructor(private route: ActivatedRoute,
    private router: Router,
    private networkService: NetworkService,
    private authService: AuthenticationService) { 
      this.networks$ = this.networkService.networks$;
      this.networkService.getAll(true).subscribe();
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
