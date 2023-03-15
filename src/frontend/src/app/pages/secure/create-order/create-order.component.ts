import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { CreateOrderService } from './services/create-address.service';

@Component({
  selector: 'app-create-order',
  templateUrl: './create-order.component.html',
  styleUrls: ['./create-order.component.scss'],
  providers: [
    CreateOrderService
  ]
})
export class CreateOrderComponent implements OnInit {

  public form = new FormGroup({
    firstName: new FormControl(null, [Validators.minLength(1), Validators.maxLength(50), Validators.required]),
    lastName: new FormControl(null, [Validators.minLength(1), Validators.maxLength(50), Validators.required]),
    email: new FormControl(null, [Validators.required]),
  });

  constructor(
    private readonly msg: MessageService,
    private readonly service: CreateOrderService
  ) { }

  public isSaving = false;

  public ngOnInit(): void {
    this.service.getAddresses().subscribe(res => {
      console.log(res);
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
