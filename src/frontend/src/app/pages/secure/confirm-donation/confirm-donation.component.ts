import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { environment } from 'src/environments/environment';
import { DONATE_FORM_DATA_KEY, DonateFormData } from '../../home/donate/stepper/services/stepper.service';
import { MessageService } from 'primeng/api';
import { DONATE_FORM_TEMP_LIST_KEY } from '../../home/donate/stepper/services/step-1.service';
import { DonatePayload } from '../../home/donate/stepper/interfaces/donate-payload';
import { DonateResponse } from '../../home/donate/stepper/interfaces/donate-response';

@Component({
  selector: 'app-confirm-donation',
  templateUrl: './confirm-donation.component.html',
  styleUrls: ['./confirm-donation.component.scss']
})
export class ConfirmDonationComponent implements OnInit {

  constructor(
    public readonly auth: AuthService,
    private readonly httpClient: HttpClient,
    private readonly msg: MessageService,
  ) { }

  public formData: DonateFormData | undefined;
  public donateNumber = '';

  public ngOnInit(): void {
    this.formData = this.load();

    if (!this.formData) {
      this.msg.add({ severity: 'error', summary: 'Error', detail: 'No form data found' });
      return;
    }

    this.donate();
  }

  public donate() {
    const payload = {
      warehouseId: this.formData?.warehouse.id,
      products: this.formData?.items.map((item) => ({
        id: item.product.id,
        quantity: item.quantity,
      })),
    } as DonatePayload;

    this.httpClient.post<DonateResponse>(`${environment.api}/donate-form`, payload).subscribe({
      next: (res) => {
        this.msg.add({ severity: 'success', summary: 'Success', detail: 'We accepted your donation!' });

        sessionStorage.removeItem(DONATE_FORM_TEMP_LIST_KEY);
        localStorage.removeItem(DONATE_FORM_DATA_KEY);

        this.donateNumber = res.donateNumber;
      },
      error: (err) => {
        this.msg.add({ severity: 'error', summary: 'Error', detail: 'We cant process your donation!' });
        localStorage.removeItem(DONATE_FORM_DATA_KEY);
        this.formData = undefined;
      }
    });
  }

  private load(): DonateFormData | undefined {
    const data = localStorage.getItem(DONATE_FORM_DATA_KEY);
    if (!data) {
      return undefined;
    }

    return JSON.parse(data);
  }

}
