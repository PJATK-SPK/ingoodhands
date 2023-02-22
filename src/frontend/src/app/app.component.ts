import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first, Observable, of, switchMap, tap } from 'rxjs';
import { AuthService } from './services/auth.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { DbUser } from './interfaces/db-user';
import { DateTime, Settings } from 'luxon';

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
    Settings.defaultLocale = 'en-GB';
    Settings.defaultZone = 'Europe/Warsaw';

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
          ? this.postlogin()
          : of({} as DbUser)),
        tap((postlogin) => {
          this.auth.updateDbUser(postlogin);
          this.authChecked = true;
        }),
        switchMap(() => this.loadGoogleMaps()),
        first()
      );
    return authCheck;
  }

  private postlogin(): Observable<DbUser> {
    const lastShotDate = sessionStorage.getItem('postloginDate');
    const now = DateTime.local();

    if (!lastShotDate || now.toSeconds() - DateTime.fromISO(lastShotDate).toSeconds() > 60) {
      sessionStorage.setItem('postloginDate', now.toISO());
      return this.httpClient.get<DbUser>(`${environment.api}/auth/postlogin`)
    }

    return of(this.auth.dbUser);
  }

  private loadGoogleMaps(): Observable<any> {
    return new Observable((observer) => {
      const script = document.createElement('script');
      script.type = 'text/javascript';
      script.src = `https://maps.googleapis.com/maps/api/js?key=${environment.googleMapsKey}&callback=nop`;
      script.onload = () => {
        observer.next();
        observer.complete();
      };
      document.body.appendChild(script);
    });
  }
}
