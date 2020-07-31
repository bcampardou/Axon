import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { I18nService } from '@app/services/i18n.service';
import { SearchService, KnowledgeSheetService } from '@app/services';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BehaviorSubject } from 'rxjs';
import { KnowledgeSheet } from '@app/models';

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
  public knowledgeBase$: BehaviorSubject<Array<KnowledgeSheet>>;
  @ViewChild('interventionModal', { static: false }) interventionModal: any;
  @ViewChild('newSheetModal', { static: false }) newSheetModal: any;

  public get language() {
    return this.i18nService.language;
  }

  public set language(value: string) {
    this.i18nService.language = value;
  }

  constructor(private router: Router,
    public i18nService: I18nService,
    private ksService: KnowledgeSheetService,
    private modalService: NgbModal,
    public search: SearchService) {
      this.knowledgeBase$ = this.ksService.knowledgeBase$;
      this.ksService.getBase(false).subscribe();
     }

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

  newSheet() {
    const ref = this.modalService.open(this.newSheetModal, { centered: true, size: 'lg', backdrop: 'static' });
  }
}
