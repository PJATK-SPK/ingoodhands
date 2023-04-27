import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { catchError, throwError } from 'rxjs';
import { DbUser } from 'src/app/interfaces/db-user';
import { AuthService } from 'src/app/services/auth.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrls: ['./user-settings.component.scss']
})
export class UserSettingsComponent {

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
  ) { }

  public isSaving = false;

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
