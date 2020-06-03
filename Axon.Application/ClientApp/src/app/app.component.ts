import { Component, OnInit } from '@angular/core';
import { I18nService } from './services/i18n.service';
import { environment } from '@env/environment';
import { merge } from 'rxjs';
import { map, filter, mergeMap } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'axon';

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router,
    private i18nService: I18nService,
    private translateService: TranslateService) {

  }

  public ngOnInit() {
    
    this.i18nService.init(environment.defaultLanguage, environment.supportedLanguages);

    const onNavigationEnd = this.router.events.pipe(filter(event => event instanceof NavigationEnd));

    // Change page title on navigation or language change, based on route data
    merge(this.translateService.onLangChange, onNavigationEnd)
      .pipe(
        map(() => {
          let route = this.activatedRoute;
          while (route.firstChild) {
            route = route.firstChild;
          }
          return route;
        }),
        filter(route => route.outlet === 'primary'),
        mergeMap(route => route.data)
      )
      .subscribe(event => {
      });
  }
}
