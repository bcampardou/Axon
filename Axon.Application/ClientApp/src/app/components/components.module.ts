import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarComponent } from './sidebar/sidebar.component';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NetworksComponent } from '../pages/networks/networks.component';
import { NetworkEditorComponent } from './networks/network-editor.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NetworkListComponent } from './networks/list/network-list.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent,
    NetworkEditorComponent,
    NetworkListComponent
  ],
  exports: [
    FooterComponent,
    NavbarComponent,
    SidebarComponent,
    NetworkEditorComponent,
    NetworkListComponent
  ]
})
export class ComponentsModule { }
