import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Role } from 'src/app/enums/role';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-manage-user',
  templateUrl: './manage-user.component.html',
  styleUrls: ['./manage-user.component.scss'],
  providers: [
  ]
})
export class ManageUserComponent {

  constructor(private readonly http: HttpClient) { }

  public ngOnInit(): void {
    this.getUser('1n7656454w').subscribe(result => console.log(result));
    this.updateUser('1n7656454w', [Role.administrator]).subscribe(result => console.log(result));
  }

  // to service
  public getUser(id: string): Observable<any> {
    return this.http.get<any>(`${environment.api}/manage-users/${id}`);
  }

  // to service
  public updateUser(id: string, roles: Role[]): Observable<any> {
    const payload = { roles };
    return this.http.patch<any>(`${environment.api}/manage-users/${id}`, payload);
  }

}
