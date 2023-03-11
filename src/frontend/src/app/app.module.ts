import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AuthInterceptor, AuthModule, LogLevel } from 'angular-auth-oidc-client';
import { environment } from 'src/environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { POST_LOGIN_ROUTE_KEY } from './services/auth.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastModule } from 'primeng/toast';
import { MessagesModule } from 'primeng/messages';
import { MessageService } from 'primeng/api';
import { AuthGuard } from './auth-guard';
import { HttpErrorInterceptor } from './services/http-error-interceptor';
import { WarehouseService } from './services/warehouse.service';
import { ServiceWorkerModule } from '@angular/service-worker';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    RouterModule,
    ToastModule,
    MessagesModule,

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
    ServiceWorkerModule.register('ngsw-worker.js', {
      enabled: environment.production,
      // Register the ServiceWorker as soon as the application is stable
      // or after 30 seconds (whichever comes first).
      registrationStrategy: 'registerWhenStable:30000'
    }),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true
    },

    AuthGuard,
    MessageService,
    WarehouseService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
