import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Coffee } from './coffee';
import { Observable, of } from 'rxjs';
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
    return this.http.post<Coffee>(this.apiUrl, coffee, this.httpOptions).pipe(
      catchError(this.errorHandler<Coffee>('addCoffee'))
    );
  }
  private errorHandler<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
