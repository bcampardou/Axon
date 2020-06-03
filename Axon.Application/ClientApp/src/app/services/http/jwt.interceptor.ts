import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '@env/environment';
import { AuthenticationService, Credentials } from '../authentication/authentication.service';
import { map } from 'rxjs/operators';

const credentialsKey = 'credentials';

/**
 * Prefixes all requests with `environment.serverUrl`.
 */
@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    private _credentials: Credentials;

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (request.url.indexOf(environment.serverUrl) >= 0) {
            let savedCredentials = sessionStorage.getItem(credentialsKey);
            if (!savedCredentials) { savedCredentials = localStorage.getItem(credentialsKey); }
            if (savedCredentials) {
                this._credentials = JSON.parse(savedCredentials);
            }

            if (!request.headers.has('Authorization') &&
                this._credentials &&
                this._credentials.token) {
                request = request.clone({
                    setHeaders: {
                        Authorization: `Bearer ${this._credentials.token}`
                    },
                    withCredentials: true
                });
            }
        }

        return next.handle(request);
    }
}
