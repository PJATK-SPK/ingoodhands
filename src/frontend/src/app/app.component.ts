import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, first, forkJoin, from, Observable, of, switchMap, tap } from 'rxjs';
import { AuthService } from './services/auth.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { DbUser } from './interfaces/db-user';
import { DateTime, Settings } from 'luxon';
import { SwPush } from '@angular/service-worker';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {

  private readonly VAPID_PUBLIC_KEY = 'BL4zflzV0yTIsPYdfwQf_0JEVjnDGg3iS37pqJqHishDblnMa6UcMr2EgVnWNS4MOBRwzruWRiWFt6WDMURK6tE';

  constructor(
    private readonly push: SwPush,
    private readonly auth: AuthService,
    private readonly router: Router,
    private readonly httpClient: HttpClient,
  ) { }

  public authChecked = false;
  public loaded = false;

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
          if (postlogin) {
            this.auth.updateDbUser(postlogin);
          }
          this.authChecked = true;
        }),
        switchMap(() => forkJoin([this.loadGoogleMaps(), this.subscribeNotifications()])),
        tap(() => this.loaded = true),
        first()
      );
    return authCheck;
  }

  private postlogin(): Observable<DbUser> {
    if (!this.auth.dbUser) {
      const savedAuthDbUser = sessionStorage.getItem('authDbUser');
      if (savedAuthDbUser) {
        this.auth.dbUser = JSON.parse(savedAuthDbUser);
      }
    }

    const authDbUserExists = this.auth.dbUser && this.auth.dbUser.id;
    const lastShotDate = sessionStorage.getItem('postloginDate');
    const now = DateTime.local();

    if (!authDbUserExists || !lastShotDate || now.toSeconds() - DateTime.fromISO(lastShotDate).toSeconds() > 60) {
      sessionStorage.setItem('postloginDate', now.toISO());
      return this.httpClient.get<DbUser>(`${environment.api}/auth/postlogin`)
        .pipe(tap(dbUser => sessionStorage.setItem('authDbUser', JSON.stringify(dbUser))))
    }

    return of(this.auth.dbUser);
  }

  private subscribeNotifications(): Observable<any> {
    if (!environment.production || !this.auth.isLoggedIn) {
      return of({});
    }

    const result = from(this.push.requestSubscription({ serverPublicKey: this.VAPID_PUBLIC_KEY }));
    const updateReq = (sub: PushSubscription | null) => {
      if (sub) {
        const keys = sub.toJSON().keys!;
        const payload = { endpoint: sub.endpoint, auth: keys['auth'], p256dh: keys['p256dh'] }
        return this.httpClient.post<any>(`${environment.api}/my-notifications/update-web-push`, payload).pipe(
          catchError(() => {
            console.error("Could not save webpush data!");
            return of(null);
          })
        );
      }
      return of(null);
    }

    const lastShotDate = sessionStorage.getItem('notificationsDate');
    const now = DateTime.local();

    if (!lastShotDate || now.toSeconds() - DateTime.fromISO(lastShotDate).toSeconds() > 60) {
      return result.pipe(
        catchError(err => {
          console.error("Could not subscribe to notifications", err);
          return of(null);
        }),
        switchMap(c => updateReq(c)),
        tap(() => sessionStorage.setItem('notificationsDate', now.toISO()))
      );
    } else {
      return of({});
    }
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
