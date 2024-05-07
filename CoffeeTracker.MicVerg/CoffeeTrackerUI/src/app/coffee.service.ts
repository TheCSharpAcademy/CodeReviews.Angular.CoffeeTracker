import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CoffeeService {
  private apiUrl: string = 'https://localhost:7288/api/coffees/';
  data: any = [];

  constructor(private http: HttpClient) { }

  //moving coffee service here
  getCoffee() {
    this.http
      .get(this.apiUrl)
      .subscribe((data: any) => {
        console.log(data);
        this.data = data;
      })
  }
}
