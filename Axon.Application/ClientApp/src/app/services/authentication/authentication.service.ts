import { Injectable } from '@angular/core';
import { Observable, of, BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '@env/environment';
import { map } from 'rxjs/operators';
import { Tenant } from '@app/models';
import { User } from '@app/models/user.model';

export interface Credentials {
  // Customize received credentials here
  user: User;
  token: string;
}

export interface LoginContext {
  tenant: string;
  login: string;
  password: string;
  remember?: boolean;
}

export interface RegisterContext {
  tenant: string;
  userName: string;
  email: string;
  password: string;
  confirmPassword: string;
  remember?: boolean;
}

const credentialsKey = 'credentials';

/**
 * Provides a base for authentication workflow.
 * The Credentials interface as well as login/logout methods should be replaced with proper implementation.
 */
@Injectable()
export class AuthenticationService {
  private url = '/users';

  public authenticatedUser$: BehaviorSubject<User> = new BehaviorSubject<User>(null);
  public tenant$: BehaviorSubject<Tenant> = new BehaviorSubject<Tenant>(null);

  public currentUser$: BehaviorSubject<User> = new BehaviorSubject<User>(null);
  public users$: BehaviorSubject<Array<User>> = new BehaviorSubject<Array<User>>([]);

  public get tenant() {
    return this.tenant$.getValue();
  }

  public get isAxonUser() {
    return this.tenant.name === 'AXON';
  }

  public get isCookiesEnabled() {
    const isCookiesEnabled = localStorage.getItem('cookies_enabled');
    return isCookiesEnabled === 'true';
  }
  public set isCookiesEnabled(value: boolean) {
    localStorage.setItem('cookies_enabled', JSON.stringify(value));
  }
  private _credentials: Credentials | null;

  constructor(private http: HttpClient) {
    this.authenticatedUser$.subscribe(user => {
        this.tenant$.next(user ? user.tenant : null);
    });
    let savedCredentials = sessionStorage.getItem(credentialsKey);
    if (!savedCredentials) { savedCredentials = localStorage.getItem(credentialsKey); }
    if (savedCredentials) {
      this._credentials = JSON.parse(savedCredentials);
      this.getUser(this._credentials.user.id).subscribe((res: User) => {
        this._credentials.user = res;
        this.setCredentials(this._credentials, true);
      });
    }
  }

  getUser(id: string) {
    return this.http.get<User>(`/users/${id}`);
  }

  post(user: User) {
    return this.http.post<User>(`/users`, user);
  }

  /**
   * Authenticates the user.
   * @param {LoginContext} context The login parameters.
   * @return {Observable<Credentials>} The user credentials.
   */
  login(context: LoginContext): Observable<Credentials> {
    return this.http.post<Credentials>(`/users/signin`, context)
      .pipe(map(credentials => {
        // login successful if there's a jwt token in the response
        this.setCredentials(credentials, context.remember);
        return credentials;
      }));
  }

  /**
   * Logs out the user and clear credentials.
   * @return {Observable<boolean>} True if the user was logged out successfully.
   */
  logout() {
    return this.http.get(`/users/signout`).pipe(
      map(res => {
        this.setCredentials(null);
        // login successful if there's a jwt token in the responsez
        return res;
      }));
  }

  /**
     * Authenticates the user.
     * @param {LoginContext} context The login parameters.
     * @return {Observable<Credentials>} The user credentials.
     */
  register(context: RegisterContext): Observable<Credentials> {
    return this.http.post<Credentials>(`/users/register`, context)
      .pipe(map(res => {
        return res;
      }));
  }

  clearCredentials() {
    this.setCredentials();
  }

  refreshToken() {
    return this.http.get<Credentials>(`/users/refreshtoken`)
      .pipe(map(user => {
        // login successful if there's a jwt token in the responsez
        this.setCredentials(user, true);
        return user;
      }));
  }

  /**
   * Checks is the user is authenticated.
   * @return {boolean} True if the user is authenticated.
   */
  isAuthenticated(): boolean {
    return !!this.credentials;
  }

  /**
   * Gets the user credentials.
   * @return {Credentials} The user credentials or null if the user is not authenticated.
   */
  get credentials(): Credentials | null {
    return this._credentials;
  }

  /**
   * Sets the user credentials.
   * The credentials may be persisted across sessions by setting the `remember` parameter to true.
   * Otherwise, the credentials are only persisted for the current session.
   * @param {Credentials=} credentials The user credentials.
   * @param {boolean=} remember True to remember credentials across sessions.
   */
  private setCredentials(credentials?: Credentials, remember?: boolean) {
    this._credentials = credentials || null;
    this.authenticatedUser$.next(this._credentials ? this._credentials.user : null);

    if (!!credentials) {
      const storage = remember ? localStorage : sessionStorage;
      storage.setItem(credentialsKey, JSON.stringify(credentials));
    } else {
      sessionStorage.removeItem(credentialsKey);
      localStorage.removeItem(credentialsKey);
    }
  }


  public get(payload: string) {
    return this.http.get<User>(`${this.url}/${payload}`).pipe(
      map(res => this.currentUser$.next(res))
    );
  }

  public getAll(force: boolean) {
    if (force || this.users$.getValue().length == 0) {
      return this.http.get<Array<User>>(`${this.url}`).pipe(
        map(res => {
          this.users$.next(res);
          return res;
        })
      );
    }
    return this.users$.asObservable();
  }

  public delete(payload: string) {
    return this.http.delete(`${this.url}/${payload}`).pipe(
      map(res => {
        let values = this.users$.getValue();
        values.splice(values.findIndex(v => v.id === payload), 1);
        this.users$.next(values);
      })
    );
  }



}
