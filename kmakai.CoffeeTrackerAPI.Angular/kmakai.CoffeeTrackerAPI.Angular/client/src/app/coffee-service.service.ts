import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Coffee } from 'src/Models/Coffee';

@Injectable({
  providedIn: 'root',
})
export class CoffeeServiceService {
  private apiUrl = 'https://localhost:7181/coffees';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(private http: HttpClient) {}

  getCoffees() {
    return this.http.get<Coffee[]>(this.apiUrl);
  }

  addCoffee(coffee: Coffee) {
    return this.http.post<Coffee>(this.apiUrl, coffee, this.httpOptions);
  }

  deleteCoffee(id: number) {
    return this.http.delete<Coffee>(this.apiUrl + '/' + id, this.httpOptions);
  }
}
