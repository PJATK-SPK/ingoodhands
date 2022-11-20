import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first, Observable, switchMap, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {

  constructor(
    public readonly auth: AuthService,
    private httpClient: HttpClient,
    private router: Router,
  ) { }

  public authChecked = false;
  public isLoggedIn = false;
  public noAuth = this.httpClient.get(`${environment.api}/test/no-auth`);
  public userinfo = this.httpClient.get(`${environment.api}/test/user-info/`);

  public ngOnInit(): void {
    this.checkAuth().subscribe();
  }

  public login(): void {
    this.auth.login();
  }

  public logout(): void {
    this.auth.logout().subscribe();
  }

  private checkAuth(): Observable<any> {
    const authCheck = this.auth.oidc.checkAuth()
      .pipe(
        tap(c => {
          if (this.auth.wantsToBeLoggedIn && !c.isAuthenticated) {
            this.auth.login();
          } else if (!c.isAuthenticated) {
            if (location.pathname !== '/') {
              this.router.navigateByUrl('/');
            }
          }

          if (this.isLoggedIn != c.isAuthenticated) {
            this.isLoggedIn = c.isAuthenticated;
          }

          this.authChecked = true;
        }),
        first()
      );
    return authCheck;
  }

}
