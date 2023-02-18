import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-manage-users',
  templateUrl: './manage-users.component.html',
  styleUrls: ['./manage-users.component.scss'],
  providers: [
  ]
})
export class ManageUsersComponent implements OnInit {

  constructor(private readonly http: HttpClient) { }

  public ngOnInit(): void {
    this.getUsers(1, 10, 'test').subscribe(result => console.log(result));
  }

  // to service
  public getUsers(page: number, pageSize: number, filter: string): Observable<any> {
    return this.http.get<any>(environment.api + '/manage-users', {
      params: {
        page: page.toString(),
        pageSize: pageSize.toString(),
        filter
      }
    });
  }

}
