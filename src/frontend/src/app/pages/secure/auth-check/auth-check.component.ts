import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-auth-check',
  templateUrl: './auth-check.component.html',
  styleUrls: ['./auth-check.component.scss']
})
export class AuthCheckComponent {

  constructor(
    public readonly auth: AuthService,
    private readonly httpClient: HttpClient,
  ) { }

  public noAuth = this.httpClient.get(`${environment.api}/test/no-auth`);
  public userinfo = this.httpClient.get(`${environment.api}/test/user-info/`);
  public dbCheck = this.httpClient.get(`${environment.api}/test/db-check/`);

  public login(): void {
    this.auth.login();
  }

  public logout(): void {
    this.auth.logout().subscribe();
  }

}
