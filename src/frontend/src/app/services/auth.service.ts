import { Injectable } from '@angular/core'
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { first, Observable, tap } from 'rxjs';
import { DbUser } from '../interfaces/db-user';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

export const POST_LOGIN_ROUTE_KEY = 'postLoginRoute';
export const WANTS_TO_BE_LOGGED_IN_KEY = 'wantsToBeLoggedIn';

@Injectable({ providedIn: 'root' })
export class AuthService {

    public isLoggedIn = false;
    public dbUser!: DbUser;

    constructor(
        public readonly oidc: OidcSecurityService,
        private readonly http: HttpClient) { }

    public get wantsToBeLoggedIn(): boolean {
        return localStorage.getItem(WANTS_TO_BE_LOGGED_IN_KEY) === 'true';
    }

    public set postLoginRoute(postLoginRoute: string | null) {
        console.info('Post login redirection set to:', postLoginRoute);
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
        sessionStorage.clear();
        localStorage.removeItem(WANTS_TO_BE_LOGGED_IN_KEY);
        this.isLoggedIn = false;
        this.dbUser = undefined as any as DbUser;
        return this.oidc.logoff().pipe(first());
    }

    public updateDbUser(details: DbUser): void {
        this.dbUser = details;
    }

    public updateDbUserFromDb(): void {
        this.http.get<DbUser>(`${environment.api}/auth/postlogin`).pipe(
            tap(dbUser => {
                sessionStorage.setItem('authDbUser', JSON.stringify(dbUser));
                this.updateDbUser(dbUser);
            }),
        ).subscribe();
    }
}