import { Component } from '@angular/core';
import { Coffee } from '../coffee';
import { CoffeeService } from '../coffeeService';

@Component({
  selector: 'app-coffee-record',
  templateUrl: './coffee-record.component.html',
  styleUrl: './coffee-record.component.css',
})
export class CoffeeRecordComponent {
  constructor(private coffeeService: CoffeeService) { }
  coffees: Coffee[] = [];

  ngOnInit(): void {
    this.getCoffees();
  }

  getCoffees(): void {
    this.coffeeService.getCoffees()
      .subscribe(coffees => {
        this.coffees = coffees;
      });
  }
}
