import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  constructor(
    public readonly auth: AuthService,
    private readonly router: Router,
  ) { }

  public login(): void {
    if (this.auth.isLoggedIn) {
      this.router.navigate(['/secure']);
    } else {
      this.auth.postLoginRoute = '/secure';
      this.auth.login();
    }
  }
}
