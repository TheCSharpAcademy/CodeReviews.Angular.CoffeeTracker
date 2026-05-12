import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CafeRecord } from '../models/cafe-record.model';

@Injectable({
  providedIn: 'root',
})
export class CafeRecordService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5171/api/cafe-records';


  // GET all cafe records, with an optional filter for product name, date and category

  getAll(drinkName?: string, date?: string, category?: number): Observable<CafeRecord[]> {
    let params = new HttpParams();
    if (drinkName) {
      params = params.set('drinkName', drinkName);
    }
    if (date) {
      params = params.set('date', date);
    }
    if (category) {
      params = params.set('category', category);
    }
    return this.http.get<CafeRecord[]>(this.apiUrl, { params });
  }

  // GET a single cafe record by ID
  getById(id: number): Observable<CafeRecord> {
    return this.http.get<CafeRecord>(`${this.apiUrl}/${id}`);
  }

  // POST a new cafe record
  create(cafeRecord: CafeRecord): Observable<CafeRecord> {
    return this.http.post<CafeRecord>(this.apiUrl, cafeRecord);
  }

  // PUT to update an existing cafe record
  update(id: number, cafeRecord: CafeRecord): Observable<CafeRecord> {
    return this.http.put<CafeRecord>(`${this.apiUrl}/${id}`, cafeRecord);
  }
  
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
