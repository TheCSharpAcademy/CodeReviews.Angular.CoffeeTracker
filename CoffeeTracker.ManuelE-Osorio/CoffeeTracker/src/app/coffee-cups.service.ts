import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable, asyncScheduler, catchError, scheduled, tap, throwError } from 'rxjs';
import { CoffeeCups, CoffeeMeasureUnits } from './coffee-cups';
import { query } from '@angular/animations';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root'
})
export class CoffeeCupsService {

  private baseUrl = "https://localhost:7245/api/CoffeeCups";

  constructor(
    private http: HttpClient,
    private notificationService: NotificationService
  ) {}

  getCoffeeCups(date?: string) : Observable<HttpResponse<CoffeeCups[]>> {
    let queryUrl: string = this.baseUrl
    if( date != null){
      queryUrl += `?date=${date}`
    }

    return this.http.get<CoffeeCups[]>(queryUrl, {
      observe: 'response',
      responseType: 'json'
    }).pipe(
      tap( {next: resp => this.log(`Items fetched succesfully`, 'success')}),
      catchError(this.logError<HttpResponse<CoffeeCups[]>>())
    );
  }

  getCoffeeCup( id: number) : Observable<HttpResponse<CoffeeCups>> {
    return this.http.get<CoffeeCups>( `${this.baseUrl}/${id}`, {
      observe: 'response',
      responseType: 'json'
    }).pipe(
      tap( {next: resp => this.log(`Item fetched succesfully`, 'success')}),
      catchError(this.logError<HttpResponse<CoffeeCups>>())
    );
  }

  postCoffeeCup( cups: CoffeeCups ) : Observable<HttpResponse<CoffeeCups>> {
    return this.http.post<CoffeeCups>(`${this.baseUrl}`, cups, {
      observe: 'response',
      responseType: 'json'
    }).pipe(
      tap( {next: (resp) => this.log(`Item created succesfully`, 'success')}),
      catchError( this.logError<HttpResponse<CoffeeCups>>())
    );
  }

  putCoffeeCup( cups: CoffeeCups ) : Observable<HttpResponse<CoffeeCups>> {
    return this.http.put<CoffeeCups>( `${this.baseUrl}/${cups.id}`, cups, {
      observe: 'response',
      responseType: 'json'
    }).pipe(
      tap( {next: (resp) => this.log(`Item modified succesfully`, 'success')}),
      catchError( this.logError<HttpResponse<CoffeeCups>>())
    );
  }

  deleteCoffeeCup( id: number ) : Observable<HttpResponse<any>> {
    return this.http.delete<HttpResponse<CoffeeCups>>( `${this.baseUrl}/${id}`, {
      observe: 'response',
      responseType: 'json'
    }).pipe(
      tap( {next: resp => this.log(`Item deleted succesfully`, 'success')}),
      catchError( this.logError<HttpResponse<CoffeeCups>>() )
    );
  }

  private log(message: string, type: string) {
    this.notificationService.add( message, type);
  }

  private logError<T>( ){
    return (error: any): Observable<T> => {
      this.log(`Unable to complete operation, please try again later. Error code: ${error.status}`, 'error');
      return scheduled([[] as T], asyncScheduler);
    };
  }
}
