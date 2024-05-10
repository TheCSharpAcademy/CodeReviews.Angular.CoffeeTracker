import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { SmokeLog } from '../models/SmokeLog';

@Injectable({
  providedIn: 'root',
})
export class SmokeService {
  private url = 'https://localhost:7065/api/SmokeLogs';

  constructor(private http: HttpClient) {}

  getLogs(): Observable<SmokeLog[]> {
    return this.http.get<SmokeLog[]>(this.url);
  }

  getLogById(id: number): Observable<SmokeLog> {
    return this.http.get<SmokeLog>(this.url + `/${id}`);
  }

  addLog(log: SmokeLog): Observable<SmokeLog> {
    return this.http.post<SmokeLog>(this.url, log);
  }

  updateLog(id: number, log: SmokeLog): Observable<SmokeLog> {
    return this.http.put<SmokeLog>(this.url + `/${id}`, log);
  }

  deleteLog(id: number): Observable<SmokeLog> {
    const url = this.url + `/${id}`;
    return this.http.delete<SmokeLog>(url);
  }
}
