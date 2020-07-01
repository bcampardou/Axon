import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { I18nService } from '@app/services/i18n.service';
import { SearchService } from '@app/services';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

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
  @ViewChild('interventionModal', { static: false }) interventionModal: any;

  public get language() {
    return this.i18nService.language;
  }

  public set language(value: string) {
    this.i18nService.language = value;
  }

  constructor(private router: Router,
    public i18nService: I18nService,
    private modalService: NgbModal,
    public search: SearchService) { }

  ngOnInit() {
    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
   });
  }
  

  setLanguage(language: string) {
    this.i18nService.language = language;
  }

  openInterventionModal() {
    const ref = this.modalService.open(this.interventionModal, { centered: true, size: 'lg', backdrop: 'static' });
  }

  performSearch() {
    this.search.search();
  }
}
