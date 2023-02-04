import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './services/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(public auth: AuthService, public router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

        if (!this.auth.isLoggedIn) {
            this.auth.postLoginRoute = state.url;
            this.auth.login();
            return false;
        }

        const haveAccess = true;
        if (!haveAccess) {
            this.router.navigate(['']);
            return false;
        }

        return true;
    }
}