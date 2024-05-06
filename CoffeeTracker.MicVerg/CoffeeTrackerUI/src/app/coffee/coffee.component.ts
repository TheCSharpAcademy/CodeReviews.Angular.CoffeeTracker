import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-coffee',
  standalone: true,
  imports: [HttpClientModule, CommonModule],
  templateUrl: './coffee.component.html',
  styleUrl: './coffee.component.css'
})
export class CoffeeComponent implements OnInit{
  httpClient = inject(HttpClient);
  apiUrl: string = 'https://localhost:7288/api/coffees/';
  data: any = [];

  ngOnInit(): void {
    this.fetchCoffee();
  }

  fetchCoffee() {
    this.httpClient
      .get(this.apiUrl)
      .subscribe((data: any) => {
        console.log(data);
        this.data = data;
      });
  }
 }