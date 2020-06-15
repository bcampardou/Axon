import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { I18nService } from '@app/services/i18n.service';
import { SearchService } from '@app/services';

declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {

  public isCollapsed = true;

  public get language() {
    return this.i18nService.language;
  }

  public set language(value: string) {
    this.i18nService.language = value;
  }

  constructor(private router: Router,
    public i18nService: I18nService,
    public search: SearchService) { }

  ngOnInit() {
    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
   });
  }
  

  setLanguage(language: string) {
    this.i18nService.language = language;
  }
}
