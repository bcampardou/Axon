import { Component, OnInit, OnDestroy } from '@angular/core';
import { environment } from '@env/environment';
import { AuthenticationService, LoginContext } from '@app/services';
import { finalize, catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { fadeInOnEnterAnimation, fadeOutOnLeaveAnimation } from 'angular-animations';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  animations: [
    fadeInOnEnterAnimation({ anchor: 'enter', duration: 1000, delay: 100 }),
    fadeOutOnLeaveAnimation({ anchor: 'leave', duration: 500 })
  ]
})
export class LoginComponent implements OnInit, OnDestroy {
  context: LoginContext = {
    tenant: '',
    login: '',
    password: '',
    remember: true
  }
  version: string = environment.version;
  error: string;
  isLoading = false;
  constructor(private router: Router,
    private toastr: ToastrService,
    private translateService: TranslateService,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
  }
  ngOnDestroy() {
  }

  onSubmit() {
    this.isLoading = true;
    this.authenticationService.login(this.context)
      .pipe(
        finalize(() => {
          this.isLoading = false;
        })
      )
      .subscribe(credentials => {
        this.router.navigate(['/'], { replaceUrl: true });
      }, res => {
        // log.debug(`Login error: ${error}`);
        this.toastr.warning(
          this.translateService.instant(res.error.message),
          this.translateService.instant('Authentication failed')
        );
      });
  }
}
