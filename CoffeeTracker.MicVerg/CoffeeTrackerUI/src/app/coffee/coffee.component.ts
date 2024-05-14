import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import { CoffeeService } from '../coffee.service';
import { Coffee } from '../coffee.model';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-coffee',
  standalone: true,
  imports: [HttpClientModule, CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './coffee.component.html',
  styleUrl: './coffee.component.css'
})
export class CoffeeComponent implements OnInit{

  constructor(private coffeeService: CoffeeService) {}

  apiUrl: string = 'https://localhost:7288/api/coffees/';
  coffeeData: Coffee[] = [];
  selectedDate?: Date;

  ngOnInit(): void {
    this.coffeeService.getCoffee().subscribe((data: Coffee[]) => {
      this.coffeeData = data;
    });
  }

  getCoffee() {
    this.coffeeService.getCoffee()
      .subscribe(coffeeData => this.coffeeData = coffeeData);
  }

  filterCoffees(selectedDate: Date): void {
    this.coffeeService.filterByDate(selectedDate)
     .subscribe(coffeeData => {
        this.coffeeData = coffeeData;
      });
  }

  onSubmit(addAmountOfCups: string) {
    let parsedAmount = parseInt(addAmountOfCups);    
    
    const newCoffee: Coffee = {
      amountOfCups: parsedAmount,
      timestamp: new Date()
    }
    this.coffeeService.addCoffee(newCoffee)
     .subscribe();
  }
  
 }
