import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { CreateAddressService } from './services/create-address.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Address } from 'src/app/interfaces/address';
import { catchError, throwError } from 'rxjs';

interface Marker {
  options: google.maps.MarkerOptions;
}

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
  public location = {
    lat: 50.1425722,
    lng: 20.8328481
  };
  marker: Marker | undefined;

  public form = new FormGroup({
    countryName: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(50)]),
    postalCode: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(10)]),
    city: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(50)]),
    street: new FormControl<string | null>(null, [Validators.maxLength(100)]),
    streetNumber: new FormControl<string | null>(null, [Validators.maxLength(10)]),
    apartment: new FormControl<string | null>(null, [Validators.maxLength(10)]),
    gpsLatitude: new FormControl<number | null>(null, [Validators.required]),
    gpsLongitude: new FormControl<number | null>(null, [Validators.required]),
  });

  constructor(
    private readonly msg: MessageService,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
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

  public onMapClick(event: google.maps.MapMouseEvent) {
    const lat = event.latLng?.lat()!;
    const lng = event.latLng?.lng()!;

    this.form.controls['gpsLatitude'].setValue(lat);
    this.form.controls['gpsLongitude'].setValue(lng);

    this.marker = this.createMarker(lat, lng);
  }


  public onMarkerClick() {
    this.form.controls['gpsLatitude'].setValue(null);
    this.form.controls['gpsLongitude'].setValue(null);

    this.marker = undefined;
  }

  public onSubmitClick(event: SubmitEvent): void {
    if (!this.form.valid) {
      const latErrors = this.form.controls['gpsLatitude'].errors;
      const lngErrors = this.form.controls['gpsLongitude'].errors;

      if (latErrors || lngErrors) {
        this.msg.add({ severity: 'error', summary: 'Error', detail: 'Please select location on map.' });
      } else {
        this.msg.add({ severity: 'error', summary: 'Error', detail: 'Please fill out all required fields.' });
      }

      return;
    }

    const payload = {
      id: '',
      countryName: this.form.get('countryName')?.value,
      postalCode: this.form.get('postalCode')?.value,
      city: this.form.get('city')?.value,
      street: this.form.get('street')?.value || null,
      streetNumber: this.form.get('streetNumber')?.value || null,
      apartment: this.form.get('apartment')?.value || null,
      gpsLatitude: this.form.get('gpsLatitude')?.value,
      gpsLongitude: this.form.get('gpsLongitude')?.value,
    } as Address;

    this.isSaving = true;

    this.service.addAddress(payload)
      .pipe(
        catchError(err => {
          this.msg.add({ severity: 'error', summary: 'Error', detail: 'Something went wrong.' });
          this.isSaving = false;
          return throwError(() => err);
        }))
      .subscribe(() => {
        setTimeout(() => {
          this.isSaving = false;
          this.msg.add({ severity: 'success', summary: 'Success', detail: 'Address has been added.' });
          setTimeout(() => {
            this.router.navigateByUrl(`/secure/create-order`);
          }, 1500);
        }, 500);
      });
  }

  private createMarker(lat: number, lng: number): Marker {
    const options = {
      draggable: false,
      position: { lat, lng },
      title: `My location`,
      icon: {
        url: `assets/img/order.png`,
        scaledSize: {
          width: 35,
          height: 35,
          equals: (_: google.maps.Size) => true
        }
      }
    };
    return { options };
  }

}
