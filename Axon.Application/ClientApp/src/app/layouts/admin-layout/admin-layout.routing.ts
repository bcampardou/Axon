import { Routes } from '@angular/router';

import { DashboardComponent } from '../../pages/dashboard/dashboard.component';
import { IconsComponent } from '../../pages/icons/icons.component';
import { MapsComponent } from '../../pages/maps/maps.component';
import { UserProfileComponent } from '../../pages/user-profile/user-profile.component';
import { TablesComponent } from '../../pages/tables/tables.component';
import { NetworksComponent } from '../../pages/networks/networks.component';
import { ServersComponent } from '../../pages/servers/servers.component';
import { ProjectsComponent } from '@app/pages/projects/projects.component';

export const AdminLayoutRoutes: Routes = [
    { path: 'dashboard',      component: DashboardComponent },
    { path: 'user-profile',   component: UserProfileComponent },
    { path: 'tables',         component: TablesComponent },
    { path: 'icons',          component: IconsComponent },
    { path: 'maps',           component: MapsComponent },
    { path: 'networks',       component: NetworksComponent },
    { path: 'servers',       component: ServersComponent },
    { path: 'projects',       component: ProjectsComponent }
];
