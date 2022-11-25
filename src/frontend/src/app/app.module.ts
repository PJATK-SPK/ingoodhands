import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AuthInterceptor, AuthModule, LogLevel } from 'angular-auth-oidc-client';
import { environment } from 'src/environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { POST_LOGIN_ROUTE_KEY } from './services/auth.service';

import { SidebarModule } from 'primeng/sidebar';
import { RippleModule } from 'primeng/ripple';
import { StyleClassModule } from 'primeng/styleclass';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SidebarModule,
    RippleModule,
    HttpClientModule,
    RouterModule,
    StyleClassModule,
    InputTextModule,
    ButtonModule,
    CardModule,

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
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
