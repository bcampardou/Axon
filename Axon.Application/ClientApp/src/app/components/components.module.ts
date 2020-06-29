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
import { TenantEditorComponent } from './tenants/tenant-editor.component';
import { TenantListComponent } from './tenants/list/tenant-list.component';
import { UserEditorComponent } from './users/user-editor.component';
import { UserListComponent } from './users/list/user-list.component';
import { AutocompleteComponent } from './autocomplete/autocomplete.component';
import { LicenseEditorComponent } from './licenses/license-editor.component';
import { LicenseListComponent } from './licenses/list/license-list.component';
import { UserSelectorComponent } from './user-selector/user-selector.component';
import { SimplemdeModule } from './simplemde/simplemde.module';
import { InterventionEditorComponent } from './interventions/editor/intervention-editor.component';
import { InterventionCalendarComponent } from './interventions/calendar/intervention-calendar.component';
import { MarkdownModule } from 'ngx-markdown';

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
    TenantEditorComponent,
    TenantListComponent,
    UserEditorComponent,
    UserListComponent,
    AutocompleteComponent,
    LicenseEditorComponent,
    LicenseListComponent,
    UserSelectorComponent,
    InterventionEditorComponent,
    InterventionCalendarComponent
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
    TenantEditorComponent,
    TenantListComponent,
    UserEditorComponent,
    UserListComponent,
    AutocompleteComponent,
    LicenseEditorComponent,
    LicenseListComponent,
    UserSelectorComponent,
    InterventionEditorComponent,
    InterventionCalendarComponent
  ]
})
export class ComponentsModule { }
