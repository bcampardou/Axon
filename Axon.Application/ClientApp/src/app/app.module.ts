import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';

import { NgbModule, NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule } from 'ngx-toastr';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
import { CoreModule } from './services';
import { CustomTranslateLoader } from './services/custom-translate-loader.service';
import { SimplemdeModule } from './components/simplemde/simplemde.module';


@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useClass: CustomTranslateLoader
      }
    }),
    SimplemdeModule.forRoot({

    }),
    ComponentsModule,
    NgbModule,
    NgbModalModule,
    ToastrModule.forRoot(),
    RouterModule,
    CoreModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    AuthLayoutComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { 
  
  constructor() {
  }
}
