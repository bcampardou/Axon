import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FullCalendarModule } from '@fullcalendar/angular';
import { SidebarComponent } from './sidebar/sidebar.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { NgbModule, NgbModalModule, NgbTabsetModule } from '@ng-bootstrap/ng-bootstrap';
import { NetworkEditorComponent } from './networks/network-editor.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NetworkListComponent } from './networks/list/network-list.component';
import { ServerListComponent } from './servers/list/server-list.component';
import { ServerEditorComponent } from './servers/server-editor.component';
import { ProjectEditorComponent } from './projects/project-editor.component';
import { ProjectListComponent } from './projects/list/project-list.component';
import { EnvironmentEditorComponent } from './environments/environment-editor.component';
import { EnvironmentListComponent } from './environments/list/environment-list.component';
import { TranslateModule } from '@ngx-translate/core';
import { UserEditorComponent } from './users/user-editor.component';
import { UserListComponent } from './users/list/user-list.component';
import { AutocompleteComponent } from './autocomplete/autocomplete.component';
import { UserSelectorComponent } from './user-selector/user-selector.component';
import { SimplemdeModule } from './simplemde/simplemde.module';
import { InterventionEditorComponent } from './interventions/editor/intervention-editor.component';
import { InterventionCalendarComponent } from './interventions/calendar/intervention-calendar.component';
import { MarkdownModule } from 'ngx-markdown';
import { SubmenuKnowledgeBaseComponent } from './sidebar/submenu-knowledge-base/submenu-knowledge-base.component';
import { KnowledgeSheetViewerComponent } from './knowledge-sheet/viewer/knowledge-sheet-viewer.component';
import { KnowledgeSheetEditorComponent } from './knowledge-sheet/editor/knowledge-sheet-editor.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgbModule,
    NgbModalModule,
    NgbTabsetModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
    SimplemdeModule,
    FullCalendarModule,
    MarkdownModule
  ],
  declarations: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent,
    NetworkEditorComponent,
    NetworkListComponent,
    ServerEditorComponent,
    ServerListComponent,
    ProjectEditorComponent,
    ProjectListComponent,
    EnvironmentEditorComponent,
    EnvironmentListComponent,
    UserEditorComponent,
    UserListComponent,
    AutocompleteComponent,
    UserSelectorComponent,
    InterventionEditorComponent,
    InterventionCalendarComponent,
    SubmenuKnowledgeBaseComponent,
    KnowledgeSheetViewerComponent,
    KnowledgeSheetEditorComponent
  ],
  exports: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent,
    NetworkEditorComponent,
    NetworkListComponent,
    ServerEditorComponent,
    ServerListComponent,
    ProjectEditorComponent,
    ProjectListComponent,
    EnvironmentEditorComponent,
    EnvironmentListComponent,
    UserEditorComponent,
    UserListComponent,
    AutocompleteComponent,
    UserSelectorComponent,
    InterventionEditorComponent,
    InterventionCalendarComponent,
    SubmenuKnowledgeBaseComponent,
    KnowledgeSheetViewerComponent,
    KnowledgeSheetEditorComponent
  ]
})
export class ComponentsModule { }
