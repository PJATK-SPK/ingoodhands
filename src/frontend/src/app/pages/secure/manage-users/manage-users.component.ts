import { Component, OnInit } from '@angular/core';
import { ManageUsersService } from './services/manage-users.service';
import { ManageUsersItem } from './models/manage-users-item';
import { PagedResult } from 'src/app/interfaces/paged-result';
import { Role } from 'src/app/enums/role';
import { Subject, debounceTime, distinctUntilChanged, takeUntil } from 'rxjs';
import { Destroy } from 'src/app/services/destroy';

@Component({
  selector: 'app-manage-users',
  templateUrl: './manage-users.component.html',
  styleUrls: ['./manage-users.component.scss'],
  providers: [
    ManageUsersService,
    Destroy,
  ]
})
export class ManageUsersComponent implements OnInit {
  public Role = Role;
  public page = 1;
  public pageSize = 10;
  public onFilterChange = new Subject<Event>();
  public filter: string | undefined;
  public pagedResult: PagedResult<ManageUsersItem> | undefined;

  constructor(
    private readonly service: ManageUsersService,
    private readonly destroy$: Destroy) { }

  public ngOnInit(): void {
    this.fetch();

    this.onFilterChange
      .pipe(
        takeUntil(this.destroy$),
        debounceTime(700),
        distinctUntilChanged()
      ).subscribe(() => this.fetch());
  }

  public paginate(event: { first: number, page: number, pageCount: number, rows: number }): void {
    this.page = event.page + 1;
    this.fetch();
  }

  private fetch(): void {
    this.service.getUsers(this.page, this.pageSize, this.filter).subscribe(result => {
      this.pagedResult = result;
    });
  }

}
