import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ClipboardModule } from 'ngx-clipboard';

import { AdminLayoutRoutes } from './admin-layout.routing';
import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { IconsComponent } from '../../pages/icons/icons.component';
import { MapsComponent } from '../../pages/maps/maps.component';
import { UserProfileComponent } from '../../pages/user-profile/user-profile.component';
import { TablesComponent } from '../../pages/tables/tables.component';
import { NgbModule, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { NetworksComponent } from '@app/pages/networks/networks.component';
import { ComponentsModule } from '@app/components/components.module';
import { ServersComponent } from '@app/pages/servers/servers.component';
import { ProjectsComponent } from '@app/pages/projects/projects.component';
import { TranslateModule } from '@ngx-translate/core';
import { UsersComponent } from '@app/pages/users/users.component';
import { TenantsComponent } from '@app/pages/tenants/tenants.component';
import { MarkdownModule } from 'ngx-markdown';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    TranslateModule,
    FormsModule,
    HttpClientModule,
    NgbModule,
    NgbModalModule,
    ComponentsModule,
    ClipboardModule,
    MarkdownModule
  ],
  declarations: [
    DashboardComponent,
    UserProfileComponent,
    TablesComponent,
    IconsComponent,
    MapsComponent,
    NetworksComponent,
    ServersComponent,
    ProjectsComponent,
    UsersComponent,
    TenantsComponent
  ]
})

export class AdminLayoutModule {}
