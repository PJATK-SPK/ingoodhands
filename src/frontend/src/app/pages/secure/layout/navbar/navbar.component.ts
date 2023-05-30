import { Component, OnInit } from '@angular/core';
import { map, mergeMap, takeUntil, timer } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { Destroy } from 'src/app/services/destroy';
import { NotificationsService } from '../../../../services/notifications.service';
import { DateTime } from 'luxon';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  providers: [Destroy]
})
export class NavbarComponent implements OnInit {
  private readonly notificationsTimer$ = timer(0, 15 * 1000).pipe(takeUntil(this.destroy$), mergeMap(() => this.service.updateNotifications()));
  public readonly picture = this.auth.oidc.getUserData().pipe(map(data => data?.picture));
  public readonly name = this.auth.oidc.getUserData().pipe(map(data => data?.name));
  public displayNotifications = false;
  public DateTime = DateTime;

  constructor(
    public readonly auth: AuthService,
    public readonly service: NotificationsService,
    private readonly destroy$: Destroy,
  ) { }

  public ngOnInit(): void {
    this.service.init();
    this.notificationsTimer$.subscribe();
  }
}
