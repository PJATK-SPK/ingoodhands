import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { getSidebarConfig } from './sidebar-config';
import { HttpClient } from '@angular/common/http';
import { Role } from 'src/app/enums/role';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent {

  public readonly config = getSidebarConfig(this.http);
  public readonly picture = this.auth.oidc.getUserData().pipe(map(data => data?.picture));
  public readonly name = this.auth.oidc.getUserData().pipe(map(data => data?.name));

  constructor(
    public readonly router: Router,
    public readonly auth: AuthService,
    public readonly http: HttpClient,
  ) { }

  public onLogoutClick() {
    this.auth.logout().subscribe();
  }

  public userHasRole(role: Role) {
    return this.auth.dbUser.roles.includes(role);
  }
}
