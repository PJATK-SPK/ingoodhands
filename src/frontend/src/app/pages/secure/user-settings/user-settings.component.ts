import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { catchError, takeUntil, throwError } from 'rxjs';
import { DbUser } from 'src/app/interfaces/db-user';
import { AuthService } from 'src/app/services/auth.service';
import { Destroy } from 'src/app/services/destroy';
import { NotificationsService } from 'src/app/services/notifications.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrls: ['./user-settings.component.scss'],
  providers: [Destroy]
})
export class UserSettingsComponent implements OnInit {

  public form = new FormGroup({
    firstName: new FormControl(this.auth.dbUser.firstName, [Validators.minLength(1), Validators.maxLength(50), Validators.required]),
    lastName: new FormControl(this.auth.dbUser.lastName, [Validators.minLength(1), Validators.maxLength(50), Validators.required]),
    phoneNumber: new FormControl(this.auth.dbUser.phoneNumber, [Validators.maxLength(50)]),
    email: new FormControl({ value: this.auth.dbUser.email, disabled: true }, [Validators.required]),
  });

  constructor(
    public readonly auth: AuthService,
    private readonly msg: MessageService,
    private readonly http: HttpClient,
    private readonly notifications: NotificationsService,
    private readonly destroy$: Destroy,
  ) { }

  public isSaving = false;
  public isResetting = false;
  public isTesting = false;
  public readNotificationsCount = 0;

  public ngOnInit(): void {
    this.readNotificationsCount = this.notifications.getReadNotifications().length;
    this.notifications.markAsReadClicked
      .pipe(takeUntil(this.destroy$))
      .subscribe(() => {
        this.readNotificationsCount = this.notifications.getReadNotifications().length;
      });
  }

  public onResetClick(): void {
    this.isResetting = true;
    setTimeout(() => {
      this.msg.add({ severity: 'success', summary: 'Success', detail: 'Browser notification data cleared!' });
      this.isResetting = false;
      this.notifications.clear();
      this.readNotificationsCount = this.notifications.getReadNotifications().length;
    }, 500);
  }

  public onTestClick(): void {
    this.http.get(`${environment.api}/my-notifications/test-web-push`)
      .pipe(
        catchError(err => {
          this.isTesting = false;
          return throwError(() => err);
        }))
      .subscribe(res => {
        setTimeout(() => {
          this.isTesting = false;
          this.msg.add({ severity: 'success', summary: 'Success', detail: 'WebPush request sent!' });
        }, 500);
      });
  }

  public onSubmitClick(event: SubmitEvent): void {
    if (!this.form.valid) {
      this.msg.add({ severity: 'error', summary: 'Error', detail: 'Please fill out all required fields.' });
      return;
    }

    const payload = {
      firstName: this.form.get('firstName')?.value,
      lastName: this.form.get('lastName')?.value,
      phoneNumber: this.form.get('phoneNumber')?.value,
    }

    this.isSaving = true;
    this.http.patch<DbUser>(`${environment.api}/user-settings/${this.auth.dbUser.id}`, payload)
      .pipe(
        catchError(err => {
          this.isSaving = false;
          return throwError(() => err);
        }))
      .subscribe(res => {
        setTimeout(() => {
          this.auth.updateDbUser(res);
          this.isSaving = false;
          this.msg.add({ severity: 'success', summary: 'Success', detail: 'Your data have been updated.' });
        }, 500);
      });
  }

}
