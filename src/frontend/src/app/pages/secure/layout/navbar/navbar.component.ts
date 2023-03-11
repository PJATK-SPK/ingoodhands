import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { Destroy } from 'src/app/services/destroy';
import { NavbarNotificationService } from './services/navbar-notification.service';
import { DateTime } from 'luxon';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  providers: [Destroy, NavbarNotificationService]
})
export class NavbarComponent implements OnInit {
  public readonly picture = this.auth.oidc.getUserData().pipe(map(data => data?.picture));
  public readonly name = this.auth.oidc.getUserData().pipe(map(data => data?.name));
  public displayNotifications = false;
  public DateTime = DateTime;

  constructor(
    public readonly auth: AuthService,
    public readonly service: NavbarNotificationService,
  ) { }

  public ngOnInit(): void {
    this.service.init();
  }

}
