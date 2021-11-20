import { Injectable } from "@angular/core";
import { CanActivate } from "@angular/router";
import { Observable, of } from "rxjs";
import { switchMap, catchError } from "rxjs/operators";
import { HttpErrorResponse } from "@angular/common/http";
import { AuthService } from "../services/auth.service";


@Injectable({
    providedIn: 'root'
})
export class AuthGuardService  implements CanActivate {
    constructor(private authService: AuthService,) {}

    canActivate(): Observable<boolean> {
      return this.authService.getUser().pipe(
          switchMap(user => {
              if (!user) {
                  this.authService.login();
                  return of(false);
              }

              return !user.expired ? of(true) : this.authService.renewToken().pipe(
                  switchMap(newUser => {
                      if (newUser && !newUser.expired)
                          return of(true);

                      this.authService.login();
                      return of(false);
                  }),
                  catchError((error: HttpErrorResponse) => {
                      if (this.authService.isUnauthorized(error)) {
                          this.authService.login();
                          return of(false);
                      }

                      console.error('canActivate', 'renewToken', 'catchError', { error });
                      return of(true);    // TODO: decide what to do with other errors when token expired (ex: network related errors)
                  }),
              );
          })
      );
  }
}



