import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first, Observable, of, switchMap, tap } from 'rxjs';
import { AuthService } from './services/auth.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { DbUser } from './interfaces/db-user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {

  constructor(
    private readonly auth: AuthService,
    private readonly router: Router,
    private readonly httpClient: HttpClient,
  ) { }

  public authChecked = false;

  public ngOnInit(): void {
    this.checkAuth().subscribe();
  }

  private checkAuth(): Observable<any> {
    const authCheck = this.auth.oidc.checkAuth()
      .pipe(
        tap(c => {
          if (this.auth.wantsToBeLoggedIn && !c.isAuthenticated && this.auth.postLoginRoute === location.pathname) {
            this.auth.login();
          } else if (!c.isAuthenticated) {
            if (location.pathname !== '/') {
              this.router.navigateByUrl('/');
            }
          }

          if (this.auth.isLoggedIn != c.isAuthenticated) {
            this.auth.isLoggedIn = c.isAuthenticated;
          }
        }),
        switchMap(c => c.isAuthenticated
          ? this.httpClient.get<DbUser>(`${environment.api}/auth/postlogin`)
          : of({} as DbUser)),
        tap((postlogin) => {
          this.auth.updateDbUser(postlogin);
          this.authChecked = true;
        }),
        first()
      );
    return authCheck;
  }

}
