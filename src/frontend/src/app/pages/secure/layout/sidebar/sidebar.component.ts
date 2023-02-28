import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, map, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { getSidebarConfig } from './sidebar-config';
import { HttpClient } from '@angular/common/http';
import { Role } from 'src/app/enums/role';
import { Destroy } from 'src/app/services/destroy';

export const FORCE_REFRESH_SIDEBAR = new Subject<void>();

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  providers: [Destroy],
})
export class SidebarComponent implements OnInit {

  public config = getSidebarConfig(this.http);
  public readonly picture = this.auth.oidc.getUserData().pipe(map(data => data?.picture));
  public readonly name = this.auth.oidc.getUserData().pipe(map(data => data?.name));

  constructor(
    public readonly router: Router,
    public readonly auth: AuthService,
    public readonly http: HttpClient,
    private readonly destroy$: Destroy
  ) { }

  public ngOnInit(): void {
    FORCE_REFRESH_SIDEBAR.pipe(takeUntil(this.destroy$)).subscribe(() => {
      this.config = getSidebarConfig(this.http);
    });
  }

  public onLogoutClick() {
    this.auth.logout().subscribe();
  }

  public userHasRole(role: Role) {
    return this.auth.dbUser.roles.includes(role);
  }
}
