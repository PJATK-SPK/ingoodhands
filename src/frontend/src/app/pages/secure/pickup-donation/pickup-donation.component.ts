import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Observable, catchError, of, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-pickup-donation',
  templateUrl: './pickup-donation.component.html',
  styleUrls: ['./pickup-donation.component.scss'],
  providers: [
  ]
})
export class PickupDonationComponent {

  public form = new FormGroup({
    donationName: new FormControl<string | null>(null, [Validators.min(1), Validators.max(10), Validators.required]),
  });

  public isSaving = false;

  constructor(private readonly http: HttpClient, private readonly msg: MessageService) { }

  public onSubmitClick(event: SubmitEvent): void {
    if (!this.form.valid) {
      this.msg.add({ severity: 'error', summary: 'Error', detail: 'Please fill out all required fields.' });
      return;
    }

    this.isSaving = true;
    const donationName = this.form.get('donationName')?.value!;

    this.pickUp(donationName)
      .pipe(
        catchError(err => {
          this.isSaving = false;
          return throwError(() => err);
        }))
      .subscribe(() => {
        setTimeout(() => {
          this.msg.add({ severity: 'success', summary: 'Success', detail: 'Success! Donation has been marked as delivered!' });
          this.form.reset();
          this.isSaving = false;
        }, 500);
      });
  }

  // to service
  public pickUp(donationName: string): Observable<any> {
    return this.http.post<any>(`${environment.api}/pickup-donation/${donationName}`, null, {});
  }
}
