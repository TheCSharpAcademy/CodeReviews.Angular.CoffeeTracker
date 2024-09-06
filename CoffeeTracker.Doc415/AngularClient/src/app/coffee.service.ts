/* import { Injectable } from '@angular/core';
import { Coffee } from './coffee';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable,of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CoffeeService {

  constructor(private http:HttpClient)  { }
  private coffeeUrl='api/coffees'

  addCoffee (coffee: Coffee): Observable<Coffee>{
    return this.http.post<Coffee>(this.coffeeUrl, coffee, this.httpOptions).pipe(
      tap((newCoffee: Coffee)=>this.log(`added coffee:${newCoffee}`)).
      catchError(this.handleError<Coffee>(`addCoffee`))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
  
      console.error(error);   
      this.log(`${operation} failed: ${error.message}`);      
      return of(result as T);
    };
  }
  
}
 */