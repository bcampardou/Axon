import { Component, OnInit } from '@angular/core';
import Chart from 'chart.js';

// core components
import {
  chartOptions,
  parseOptions,
  chartExample1,
  chartExample2
} from "../../variables/charts";
import { User, Network, Project, Server } from '@app/models';
import { Subscription } from 'rxjs';
import { AuthenticationService, NetworkService, ServerService, ProjectService } from '@app/services';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  public datasets: any;
  public data: any;
  public salesChart;
  public clicked: boolean = true;
  public clicked1: boolean = false;

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

    this.datasets = [
      [0, 20, 10, 30, 15, 40, 20, 60, 60],
      [0, 20, 5, 25, 10, 30, 15, 40, 40]
    ];
    this.data = this.datasets[0];


    var chartOrders = document.getElementById('chart-orders');

    parseOptions(Chart, chartOptions());


    var ordersChart = new Chart(chartOrders, {
      type: 'bar',
      options: chartExample2.options,
      data: chartExample2.data
    });

    var chartSales = document.getElementById('chart-sales');

    this.salesChart = new Chart(chartSales, {
			type: 'line',
			options: chartExample1.options,
			data: chartExample1.data
		});
  }





  public updateOptions() {
    this.salesChart.data.datasets[0].data = this.data;
    this.salesChart.update();
  }

}
