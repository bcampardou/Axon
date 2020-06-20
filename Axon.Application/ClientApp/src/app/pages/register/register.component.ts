import { Component, OnInit } from '@angular/core';
import { environment } from '@env/environment';
import { LoginContext, AuthenticationService, RegisterContext } from '@app/services';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { finalize } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  context: RegisterContext = {
    tenant: '',
    userName: '',
    email: '',
    password: '',
    confirmPassword: '',
    remember: false
  }
  version: string = environment.version;
  error: string;
  isLoading = false;
  hasAgreedPolicy = false;

  constructor(
    private toastr: ToastrService,
    private router: Router,
    private translateService: TranslateService,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
  }

  signUp() {
    this.isLoading = true;
    this.authenticationService.register(this.context)
      .pipe(finalize(() => {
        this.isLoading = false;
      }))
      .subscribe(credentials => {

        this.router.navigateByUrl('/login').then(() => {
          this.toastr.success(this.translateService.instant('You will receive an email when your request will be treated'), this.translateService.instant('Your request has been sent'));
        });
      }, res => {
        // log.debug(`Register error: ${res}`);
        for (let i in res.error.details) {
          this.toastr.error(
            this.translateService.instant(res.error.details[i]), this.translateService.instant(res.error.message)
          )
        }

      });
  }

}
