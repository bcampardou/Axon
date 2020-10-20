import { Routes } from '@angular/router';

import { DashboardComponent } from '@app/pages/dashboard/dashboard.component';
import { IconsComponent } from '@app/pages/icons/icons.component';
import { MapsComponent } from '@app/pages/maps/maps.component';
import { UserProfileComponent } from '@app/pages/user-profile/user-profile.component';
import { TablesComponent } from '@app/pages/tables/tables.component';
import { NetworksComponent } from '@app/pages/networks/networks.component';
import { ServersComponent } from '@app/pages/servers/servers.component';
import { ProjectsComponent } from '@app/pages/projects/projects.component';
import { UsersComponent } from '@app/pages/users/users.component';
import { KnowledgeSheetComponent } from '@app/pages/knowledge-sheet/knowledge-sheet.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'user-profile',   component: UserProfileComponent },
    { path: 'tables',         component: TablesComponent },
    { path: 'icons',          component: IconsComponent },
    { path: 'maps',           component: MapsComponent },
    { path: 'networks',       component: NetworksComponent },
    { path: 'servers',       component: ServersComponent },
    { path: 'projects',       component: ProjectsComponent },
    { path: 'users',       component: UsersComponent },
    { path: 'knowledge/:id',     component: KnowledgeSheetComponent }
];
