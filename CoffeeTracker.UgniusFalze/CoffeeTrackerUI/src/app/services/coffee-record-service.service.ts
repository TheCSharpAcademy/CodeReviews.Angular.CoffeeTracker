import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { CoffeeRecord } from '../CoffeeRecord';

@Injectable({
  providedIn: 'root'
})
export class CoffeeRecordServiceService {

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  private apiUrl:string = 'https://localhost:7106/api/Coffee/'

 constructor(private http:HttpClient) { }
 
 getCoffeeRecords() : Observable<CoffeeRecord[]> {
  return this.http.get<CoffeeRecord[]>(this.apiUrl).pipe(
    map((data) => {
      data.map((record) => {
        record.recordDate = new Date(record.recordDate);
        return record;
      })
      return data;
    })
  );
 }
}
