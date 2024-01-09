import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { IRecords } from './irecords';
import { Record } from './records.modal';

@Injectable({
  providedIn: 'root'
})
export class RecordsService {
  private apiURL = "https://localhost:7124/api/Records";

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private httpClient: HttpClient) { }

  getRecords(): Observable<IRecords[]> {
    return this.httpClient.get<IRecords[]>(this.apiURL + '/GetRecords').pipe(
      catchError(this.handleError<IRecords[]>('GetRecords', []))
    );
  };

  addRecord(record: Record): Observable<Record> {
    console.log(JSON.stringify(record, null, 2));
    return this.httpClient.post<Record>(this.apiURL + '/NewRecord', JSON.stringify(record, null, 2), this.httpOptions).pipe(
      catchError(this.handleError<Record>('addRecord'))
    );
  };

  findRecord(id: number): Observable<Record> {
    const url = `${this.apiURL}/GetRecord/${id}`;
    return this.httpClient.get<Record>(url).pipe(
      catchError(this.handleError<Record>(`FindRecord id=${id}`))
    );
  };

  updateRecord(id: number, record: Record): Observable<any> {
    const url = `${this.apiURL}/UpdateRecord/${id}`;
    return this.httpClient.put(url, record, this.httpOptions).pipe(
      catchError(this.handleError<any>('updateRecord'))
    );
  };

  deleteRecord(id: number) {
    const url = `${this.apiURL}/DeleteRecord/${id}`;
    return this.httpClient.delete<IRecords>(url, this.httpOptions).pipe(
      catchError(this.handleError<IRecords>('deleteRecord'))
    );
  };

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  };
}
