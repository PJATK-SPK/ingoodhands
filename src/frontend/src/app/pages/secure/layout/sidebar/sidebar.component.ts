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

  public getBgClassByRole(role: Role | 'all') {
    let result = 'border-transparent';

    if (role == Role.administrator) {
      result += ' bg-gray-50';
    } else if (role == Role.warehouseKeeper) {
      result += ' bg-green-50';
    } else if (role == Role.deliverer) {
      result += ' bg-red-50';
    } else if (role == Role.donor) {
      result += ' bg-yellow-50';
    } else if (role == Role.needy) {
      result += ' bg-blue-50';
    }

    return result;
  }

  public onLogoutClick() {
    this.auth.logout().subscribe();
  }

  public userHasRole(role: Role) {
    return this.auth.dbUser.roles.includes(role);
  }
}
