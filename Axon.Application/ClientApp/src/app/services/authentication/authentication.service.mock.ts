// import { Observable, of } from 'rxjs';

// import { Credentials, LoginContext } from './authentication.service';
// import { User } from '@app/core/models';

// export class MockAuthenticationService {
//   get user(): User {
//     let user = new User();
//     user.userName = "test";
//     return user;
//   }
//   credentials: Credentials | null = {
//     user: this.user,
//     token: '123'
//   };

//   login(context: LoginContext): Observable<Credentials> {
//     return of({
//       user: this.user,
//       token: '123456'
//     });
//   }

//   logout(): Observable<boolean> {
//     this.credentials = null;
//     return of(true);
//   }

//   isAuthenticated(): boolean {
//     return !!this.credentials;
//   }

// }
