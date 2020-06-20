import { Component, OnInit } from '@angular/core';
import Chart from 'chart.js';

import { User, Network, Project, Server } from '@app/models';
import { Subscription } from 'rxjs';
import { AuthenticationService, NetworkService, ServerService, ProjectService } from '@app/services';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public users: Array<User> = [];
  public networks: Array<Network> = [];
  public servers: Array<Server> = [];
  public projects: Array<Project> = [];

  private _subscriptions = new Array<Subscription>();
  

  constructor(private authService: AuthenticationService,
    private networkService: NetworkService,
    private serverService: ServerService,
    private projectService: ProjectService) { 
      this._subscriptions.push(
        this.authService.getAll(false).subscribe(users => this.users = users)
      );
      this._subscriptions.push(
        this.networkService.getAll(false).subscribe(networks => this.networks = networks)
      );
      this._subscriptions.push(
        this.serverService.getAll(false).subscribe(servers => this.servers = servers)
      );
      this._subscriptions.push(
        this.projectService.getAll(false).subscribe(projects => this.projects = projects)
      );

  }

  ngOnInit() {

    
  }

}
