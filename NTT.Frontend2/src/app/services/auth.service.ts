import { Injectable } from '@angular/core';
import { Observable, of, from, Subscription } from 'rxjs';
import { UserManager, User } from 'oidc-client';
// import { AppConfigService } from './app-config.service';
import { HttpErrorResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
// import * as actionsUser from '../store/actions/user.action';

@Injectable()
export class AuthService {
  constructor(
    // appConfig: AppConfigService,
  ) {
    this._userManager = new UserManager({
      authority: environment.identityProvider.stsAuthority,
      client_id: environment.identityProvider.clientId,
      redirect_uri: `${environment.identityProvider.clientRoot}assets/oidc-client/signin-callback.html`,
      post_logout_redirect_uri: `${environment.identityProvider.clientRoot}assets/oidc-client/signout-callback.html`,
      silent_redirect_uri: `${environment.identityProvider.clientRoot}assets/oidc-client/silent-callback.html`,
      automaticSilentRenew: true,
      response_type: 'code',
      scope: environment.identityProvider.clientScope,
    });
    // this._userManager.settings.extraQueryParams = {"ngsw-bypass": true};
  }

  private _userManager: UserManager;
  private _user: User;
  private _subscription: Subscription;
  private _renewToken$: Observable<User>;

  getUser(): Observable<User> {
    if (this._user && !this._user.expired)
      return of(this._user);

    const getUser$ = from(this._userManager.getUser());

    if (this._subscription && !this._subscription.closed)
      this._subscription.unsubscribe();
    this._subscription = getUser$.subscribe(user => this.setUser(user));

    return getUser$;
  }

  login(args?: any): Observable<any> {
    this.setUser(null);
    args = Object.assign({}, args, { url: (args && args.url) || (document && document.location && document.location.toString()) });
    return from(this._userManager.signinRedirect(args && { state: args }));
  }

  renewToken(args?: any): Observable<User> {
    if (!this._renewToken$) {
      this._renewToken$ = from(this._userManager.signinSilent(args));
      const subscription = this._renewToken$.subscribe(user => {
        subscription.unsubscribe();
        this.setUser(user);
        this._renewToken$ = null;
      });
    }

    return this._renewToken$;
  }

  logout(args?: any): Observable<any> {
    this.setUser(null);
    if (args)
      return from(this._userManager.signoutRedirect(args));
    else
      return from(this._userManager.signoutRedirect());
  }

  isUnauthorized(error: HttpErrorResponse) {
    return error && (error.error === 'login_required' || (error.status === 401 && error.statusText === 'Unauthorized'));
  }

  private setUser(user: User): void {
    this._user = user;
    // this.store.dispatch(user && !user.expired ? new actionsUser.RequestUser() : new actionsUser.RequestUserSuccess(null));
  }
}
