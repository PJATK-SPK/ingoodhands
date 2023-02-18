import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-stocks-donation',
  templateUrl: './stocks.component.html',
  styleUrls: ['./stocks.component.scss'],
  providers: [
  ]
})
export class StocksComponent {
  public page = 1;
  public pageSize = 10;

  constructor(private readonly http: HttpClient) { }

  public ngOnInit(): void {
    this.fetch().subscribe(result => console.log(result));
  }

  // to service
  public fetch(): Observable<any> {
    return this.http.get<any>(`${environment.api}/stocks`, {
      params: {
        page: this.page.toString(),
        pageSize: this.pageSize.toString()
      }
    });
  }
}
