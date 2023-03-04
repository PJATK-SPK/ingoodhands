import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ManageUserDetailsService } from './services/manage-users.service';
import { ManageUserDetails } from './models/manage-user-details';
import { WarehouseService } from 'src/app/services/warehouse.service';
import { Warehouse } from 'src/app/interfaces/warehouse';
import { FormControl, FormGroup } from '@angular/forms';
import { Role } from 'src/app/enums/role';
import { MessageService } from 'primeng/api';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-manage-user',
  templateUrl: './manage-user.component.html',
  styleUrls: ['./manage-user.component.scss'],
  providers: [
    ManageUserDetailsService,
  ]
})
export class ManageUserComponent implements OnInit {
  public id!: string;
  public warehouses: Warehouse[] = [];
  public filteredWarehouses: Warehouse[] = [];
  public isSaving = false;
  public userFullName?: string;

  public form = new FormGroup({
    warehouseId: new FormControl<{ id: string, name: string } | null>(null),
    isAdministrator: new FormControl(false),
    isDonor: new FormControl(false),
    isNeedy: new FormControl(false),
    isWarehouseKeeper: new FormControl(false),
    isDeliverer: new FormControl(false),
  });

  public formRoles = [
    { ctrl: 'isAdministrator', name: 'Administrator' },
    { ctrl: 'isDonor', name: 'Donor' },
    { ctrl: 'isNeedy', name: 'Needy' },
    { ctrl: 'isWarehouseKeeper', name: 'Warehouse keeper' },
    { ctrl: 'isDeliverer', name: 'Deliverer' },
  ]

  constructor(
    private readonly route: ActivatedRoute,
    private readonly service: ManageUserDetailsService,
    private readonly warehouseService: WarehouseService,
    private readonly msg: MessageService,
    private readonly http: HttpClient,
  ) { }

  public ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id')!;

    this.warehouseService.getAll().subscribe(warehouses => {
      this.warehouses = warehouses;

      this.service.getUser(this.id).subscribe(user => {
        this.userFullName = user.fullName;
        this.patchForm(user);
      });
    });
  }

  public filterWarehouse(event: { originalEvent: PointerEvent, query: string }) {
    const filtered: Warehouse[] = [];

    this.warehouses.forEach(item => {
      if (item.name.toLowerCase().indexOf(event.query.toLowerCase()) == 0) {
        filtered.push(item);
      }
    });

    this.filteredWarehouses = filtered;
  }

  public patchForm(details: ManageUserDetails) {
    const warehouse = this.warehouses.find(c => c.id === details.warehouseId);

    this.form.patchValue({
      warehouseId: details.warehouseId ? { name: warehouse?.name!, id: warehouse?.id! } : null,
      isAdministrator: details.roles.includes(Role.administrator),
      isDonor: details.roles.includes(Role.donor),
      isNeedy: details.roles.includes(Role.needy),
      isWarehouseKeeper: details.roles.includes(Role.warehouseKeeper),
      isDeliverer: details.roles.includes(Role.deliverer),
    })
  }

  public onSubmit(event: SubmitEvent) {
    const payload = {
      warehouseId: this.form.get('warehouseId')?.value?.id ?? null,
      roles: [] as Role[],
    }

    if (this.form.get('isAdministrator')?.value) payload.roles.push(Role.administrator);
    if (this.form.get('isDonor')?.value) payload.roles.push(Role.donor);
    if (this.form.get('isNeedy')?.value) payload.roles.push(Role.needy);
    if (this.form.get('isWarehouseKeeper')?.value) payload.roles.push(Role.warehouseKeeper);
    if (this.form.get('isDeliverer')?.value) payload.roles.push(Role.deliverer);

    this.isSaving = true;

    this.http.patch<ManageUserDetails>(`${environment.api}/manage-users/${this.id}`, payload).subscribe(res => {
      setTimeout(() => {
        this.isSaving = false;
        this.msg.add({ severity: 'success', summary: 'Success', detail: 'Done!' });
      }, 500);
    });
  }
}
