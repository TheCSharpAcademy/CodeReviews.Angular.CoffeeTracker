import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, map, filter } from 'rxjs';
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

  filterByDate(selectedDate: Date): Observable<Coffee[]> {
    if (!selectedDate){
      return of([]);
    }
    return this.http.get<Coffee[]>(this.apiUrl)
   .pipe(
        map(coffeeData =>
          coffeeData.filter(coffee => {
            const coffeeDateMidnight = new Date(new Date(coffee.timestamp).setHours(0, 0, 0, 0));
            const selectedDateMidnight = new Date(selectedDate.setHours(0, 0, 0, 0));

            return coffeeDateMidnight.getTime() === selectedDateMidnight.getTime();
          })
        )
      );
  }  
 }

