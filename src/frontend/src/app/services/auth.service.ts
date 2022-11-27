import { Injectable } from '@angular/core'
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { first, Observable } from 'rxjs';

export const POST_LOGIN_ROUTE_KEY = 'postLoginRoute';
export const WANTS_TO_BE_LOGGED_IN_KEY = 'wantsToBeLoggedIn';

@Injectable({ providedIn: 'root' })
export class AuthService {

    public isLoggedIn = false;

    constructor(public readonly oidc: OidcSecurityService) { }

    public get wantsToBeLoggedIn(): boolean {
        return localStorage.getItem(WANTS_TO_BE_LOGGED_IN_KEY) === 'true';
    }

    public set postLoginRoute(postLoginRoute: string | null) {
        localStorage.setItem(POST_LOGIN_ROUTE_KEY, postLoginRoute!);
    }

    public get postLoginRoute(): string | null {
        return localStorage.getItem(POST_LOGIN_ROUTE_KEY);
    }

    public login(): void {
        localStorage.setItem(WANTS_TO_BE_LOGGED_IN_KEY, 'true');
        this.oidc.authorize();
    }

    public logout(): Observable<unknown> {
        localStorage.removeItem(WANTS_TO_BE_LOGGED_IN_KEY);
        return this.oidc.logoff().pipe(first());
    }
}