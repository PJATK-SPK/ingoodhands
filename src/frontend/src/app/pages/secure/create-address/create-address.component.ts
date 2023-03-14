import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-create-address',
  templateUrl: './create-address.component.html',
  styleUrls: ['./create-address.component.scss'],
  providers: [
  ]
})
export class CreateAddressComponent {

  public form = new FormGroup({
    firstName: new FormControl(null, [Validators.min(1), Validators.max(50), Validators.required]),
    lastName: new FormControl(null, [Validators.min(1), Validators.max(50), Validators.required]),
    email: new FormControl(null, [Validators.required]),
  });

  constructor(
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
    }

    this.isSaving = true;
    // this.http.patch<DbUser>(`${environment.api}/user-settings/${this.auth.dbUser.id}`, payload)
    //   .pipe(
    //     catchError(err => {
    //       this.isSaving = false;
    //       return throwError(() => err);
    //     }))
    //   .subscribe(res => {
    //     setTimeout(() => {
    //       this.auth.updateDbUser(res);
    //       this.isSaving = false;
    //       this.msg.add({ severity: 'success', summary: 'Success', detail: 'Your data have been updated.' });
    //     }, 500);
    //   });
  }

}
