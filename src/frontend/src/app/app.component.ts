import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first, Observable, tap } from 'rxjs';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {

  constructor(
    private readonly auth: AuthService,
    private readonly router: Router,
  ) { }

  public authChecked = false;

  public ngOnInit(): void {
    this.checkAuth().subscribe();
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

          if (this.auth.isLoggedIn != c.isAuthenticated) {
            this.auth.isLoggedIn = c.isAuthenticated;
          }

          this.authChecked = true;
        }),
        first()
      );
    return authCheck;
  }

}
