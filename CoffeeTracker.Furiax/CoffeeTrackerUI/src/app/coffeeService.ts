import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Coffee } from './coffee';
import { Observable, of, map } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class CoffeeService {

  readonly apiUrl = 'http://localhost:5270/api/coffee'; // link to api
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  constructor(private http: HttpClient) { }

  getCoffees(): Observable<Coffee[]> {
    return this.http.get<Coffee[]>(this.apiUrl).pipe(
      catchError(this.errorHandler<Coffee[]>('getCoffees', []))
    );
  }

  getCoffee(id: number): Observable<Coffee> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<Coffee>(url).pipe(
    catchError(this.errorHandler<Coffee>(`getCoffee id=${id}`))
    );
  }

getCoffeeNo404<Data>(id: number): Observable < Coffee > {
  const url = `${this.apiUrl}/${id}`;
  return this.http.get<Coffee>(url)
    .pipe(
      catchError(this.errorHandler<Coffee>(`getCoffee id=${id}`))
    );
}

  updateCoffee(coffee: Coffee): Observable<any> {
    const url = `${this.apiUrl}/${coffee.id}`;
    return this.http.put(url, coffee, this.httpOptions).pipe(
      catchError(this.errorHandler<any>('updateCoffee'))
    );
  }
  addCoffee(coffee: Coffee): Observable<Coffee> {
    console.log("service: " + coffee.time);
    return this.http.post<Coffee>(this.apiUrl, coffee, this.httpOptions).pipe(
      catchError(this.errorHandler<Coffee>('addCoffee'))
    );
  }
  deleteCoffee(id: number): Observable<Coffee> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<Coffee>(url, this.httpOptions).pipe(
      catchError(this.errorHandler<Coffee>('deleteHero'))
    );
  }

  searchCoffees(searchDate: Date): Observable<Coffee[]> {
    if (!searchDate) {
      return of([]);
    }

    return this.http.get<Coffee[]>(this.apiUrl).pipe(
      map(coffees => coffees.filter(coffee => {
        const coffeeTime = new Date(coffee.time);

        const coffeeTimeZoneOffset: number = coffeeTime.getTimezoneOffset();
        const adjustedCoffeeTime: Date = new Date(coffeeTime.getTime() - coffeeTimeZoneOffset * 60000);

        //convert searchDate and coffeeTime to string for comparison
        const selectedDate: string = searchDate.toISOString().split('T')[0];
        const recordDate: string = adjustedCoffeeTime.toISOString().split('T')[0];
        
        return(
          coffeeTime instanceof Date &&
          selectedDate === recordDate
        );
      })),

      catchError(this.errorHandler<Coffee[]>('searchCoffees', []))
    );
  }

  private errorHandler<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
