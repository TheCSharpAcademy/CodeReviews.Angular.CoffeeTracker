import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Coffee } from './coffe/coffee';

@Injectable({
  providedIn: 'root'
})
export class CoffeeService {
  private httpClient=inject(HttpClient);
  private url="https://localhost:7273/api/Coffees/"

    deleteCoffee(coffeeId:string):Observable<any>{
    return this.httpClient.delete(`${this.url}${coffeeId}`)
    }

    addCoffee(coffeeSelected:Coffee):Observable<Coffee>{
      return this.httpClient.post<Coffee>(`${this.url}`,coffeeSelected)
    }


    getCoffeesAtDate(date:string){
      return this.httpClient.get<Coffee[]>(`${this.url}${date}`)
    }
}
