import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { CoffeeService } from '../coffee.service';
import { Coffee } from '../coffee.model';

@Component({
  selector: 'app-coffee',
  standalone: true,
  imports: [HttpClientModule, CommonModule],
  templateUrl: './coffee.component.html',
  styleUrl: './coffee.component.css'
})
export class CoffeeComponent implements OnInit{

  constructor(private coffeeService: CoffeeService) {}

  apiUrl: string = 'https://localhost:7288/api/coffees/';
  coffeeData: any[] = [];

  ngOnInit(): void {
    this.coffeeService.getCoffee().subscribe((data: Coffee[]) => {
      console.log(data);
      this.coffeeData = data;
    });
  }
 }
