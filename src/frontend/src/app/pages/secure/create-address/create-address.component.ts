import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { CreateAddressService } from './services/create-address.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-create-address',
  templateUrl: './create-address.component.html',
  styleUrls: ['./create-address.component.scss'],
  providers: [
    CreateAddressService
  ]
})
export class CreateAddressComponent implements OnInit {

  public id!: string;
  public isSaving = false;
  public filteredCountries: string[] = [];
  public countries: string[] = [];

  public form = new FormGroup({
    countryName: new FormControl(null, [Validators.required]),
    lastName: new FormControl(null, [Validators.min(1), Validators.max(50), Validators.required]),
    email: new FormControl(null, [Validators.required]),
  });

  constructor(
    private readonly msg: MessageService,
    private readonly route: ActivatedRoute,
    private readonly service: CreateAddressService,
  ) { }

  public ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id')!;

    this.service.getCountries().subscribe(countries => {
      this.countries = countries;
    });
  }

  public filterCountry(event: { originalEvent: PointerEvent, query: string }) {
    const filtered: string[] = [];

    this.countries.forEach(item => {
      if (item.toLowerCase().indexOf(event.query.toLowerCase()) == 0) {
        filtered.push(item);
      }
    });

    this.filteredCountries = filtered;
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
