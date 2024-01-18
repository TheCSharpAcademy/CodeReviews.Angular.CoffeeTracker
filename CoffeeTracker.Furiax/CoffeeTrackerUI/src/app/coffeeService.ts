import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Coffee-record } from './coffee-record';

@Injectable({
  providedIn: 'root'
})

export class CoffeeService {
  constructor(private http: HttpClient) { }

  formData: coffee = new coffee();// modify this
  readonly baseUrl = 'http://localhost:5270/api/coffee'; // link to api

  postCoffeeRecord() {
    return this.http.post(this.baseUrl, this.formData);//call the post function of the api and pass the model
  }
}
