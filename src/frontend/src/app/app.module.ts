import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { LuxonDateAdapter, MAT_LUXON_DATE_ADAPTER_OPTIONS, MAT_LUXON_DATE_FORMATS } from '@angular/material-luxon-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AuthInterceptor, AuthModule, LogLevel } from 'angular-auth-oidc-client';
import { environment } from 'src/environments/environment';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { POST_LOGIN_ROUTE_KEY } from './services/auth.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,

    HttpClientModule,
    RouterModule,

    AuthModule.forRoot({
      config: {
        authority: environment.auth.authority,
        redirectUrl: environment.url,
        postLoginRoute: localStorage.getItem(POST_LOGIN_ROUTE_KEY) ?? window.location.origin,
        postLogoutRedirectUri: window.location.origin,
        clientId: environment.auth.clientId,
        scope: environment.auth.scopes,
        responseType: 'code',
        silentRenew: true,
        useRefreshToken: true,
        logLevel: environment.production ? LogLevel.None : LogLevel.Warn,
        customParamsAuthRequest: {
          audience: environment.auth.audience,
        },
        customParamsRefreshTokenRequest: {
          scope: environment.auth.scopes,
        },
        secureRoutes: [environment.api]
      },
    }),

  ],
  providers: [
    { provide: DateAdapter, useClass: LuxonDateAdapter, deps: [MAT_DATE_LOCALE] },
    { provide: MAT_DATE_FORMATS, useValue: MAT_LUXON_DATE_FORMATS },

    {
      provide: MAT_LUXON_DATE_ADAPTER_OPTIONS,
      useValue: 1, // Monday
    },

    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
