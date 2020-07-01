import { Component, OnInit, ViewChild } from '@angular/core';
import Chart from 'chart.js';

import { User, Network, Project, Server } from '@app/models';
import { Subscription } from 'rxjs';
import { AuthenticationService, NetworkService, ServerService, ProjectService } from '@app/services';
import { Intervention } from '@app/models/intervention.model';
import { InterventionService } from '@app/services/intervention.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { fadeInOnEnterAnimation, fadeOutOnLeaveAnimation } from 'angular-animations';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  animations: [
    fadeInOnEnterAnimation({ anchor: 'enter', duration: 1000, delay: 100 }),
    fadeOutOnLeaveAnimation({ anchor: 'leave', duration: 500 })
  ]
})
export class DashboardComponent implements OnInit {
  public users: Array<User> = [];
  public networks: Array<Network> = [];
  public servers: Array<Server> = [];
  public projects: Array<Project> = [];
  public interventions: Array<Intervention> = [];

  private _subscriptions = new Array<Subscription>();


  constructor(private authService: AuthenticationService,
    private networkService: NetworkService,
    private serverService: ServerService,
    private projectService: ProjectService,
    private interventionService: InterventionService,
    private modalService: NgbModal) {
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
    this._subscriptions.push(
      this.interventionService.getAll('all', true).subscribe(interventions => this.interventions = interventions)
    );

  }

  ngOnInit() {


  }

}
