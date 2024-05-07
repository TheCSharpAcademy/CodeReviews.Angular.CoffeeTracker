import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Coffee } from './coffee.model';

@Injectable({
  providedIn: 'root'
})
export class CoffeeService {
  private apiUrl: string = 'https://localhost:7288/api/coffees/';

  constructor(private http: HttpClient) { }

  getCoffee(): Observable<Coffee[]> {
    return this.http.get<Coffee[]>(this.apiUrl);
 }
}
