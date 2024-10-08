import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { CoffeeRecord } from './coffee-record.interface';
import { CreateCoffeeRecord } from './create-coffee-record.interface';

@Injectable({
  providedIn: 'root',
})
export class CoffeeRecordService {
  private url = 'https://localhost:7010/api/v1/coffees';
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };
  private _coffeeRecords = new BehaviorSubject<CoffeeRecord[]>([]);

  public CoffeeRecords = this._coffeeRecords.asObservable();

  constructor(private http: HttpClient) {}

  addCoffeeRecord(request: CreateCoffeeRecord): Observable<boolean> {
    return this.http
      .post<CoffeeRecord>(this.url, request, this.httpOptions)
      .pipe(
        map((record) => {
          this.getCoffeeRecords();
          return true;
        }),
        catchError((error) => {
          console.error('ERROR - Adding Coffee Record: ', error);
          return of(false);
        })
      );
  }

  deleteCoffeeRecord(id: string): Observable<boolean> {
    return this.http.delete<CoffeeRecord>(`${this.url}/${id}`).pipe(
      map((record) => {
        this.getCoffeeRecords();
        return true;
      }),
      catchError((error) => {
        console.error('ERROR - Deleting Coffee Record: ', error);
        return of(false);
      })
    );
  }

  editCoffeeRecord(request: CoffeeRecord): Observable<boolean> {
    return this.http
      .put<CoffeeRecord>(`${this.url}/${request.id}`, request, this.httpOptions)
      .pipe(
        map((record) => {
          this.getCoffeeRecords();
          return true;
        }),
        catchError((error) => {
          console.error('ERROR - Updating Coffee Record: ', error);
          return of(false);
        })
      );
  }

  getCoffeeRecords(): void {
    this.http.get<CoffeeRecord[]>(this.url).subscribe(
      (records) => {
        this._coffeeRecords.next(records);
      },
      (error) => {
        console.error('ERROR - Fetching Coffee Records: ', error);
      }
    );
  }
}
