import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { ServerService } from '@app/services';
import { BehaviorSubject } from 'rxjs';
import { Server } from '@app/models';

@Component({
  selector: 'app-servers',
  templateUrl: './servers.component.html',
  styleUrls: ['./servers.component.scss']
})
export class ServersComponent implements OnInit, OnDestroy {
  public isCollapsed = true;
  public servers$ = new BehaviorSubject<Array<Server>>([]);
  public mode: string = 'list';

  constructor(private router: Router,
    private serverService: ServerService) { 
      this.servers$ = this.serverService.servers$;
      this.serverService.getAll(true).subscribe();
    }

  ngOnInit() {
  }

  ngOnDestroy() {
  }

  public create() {
    this.serverService.currentServer$.next(new Server());
    this.mode = 'creation';
  }

  public show(id: string) {
    this.serverService.get(id).subscribe(res => this.mode = 'edition');
  }

  public cancelEdition(event: boolean) {
    if(event) {
      this.serverService.currentServer$.next(null);
      this.mode = 'list';
    }
  }
}
