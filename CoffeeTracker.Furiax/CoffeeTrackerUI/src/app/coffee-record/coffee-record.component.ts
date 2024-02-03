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
      .subscribe(coffees => this.coffees = coffees);
  }
  delete(coffee: Coffee): void {
    const confirm = window.confirm(`Are you sure you want to delete coffee record ${coffee.id}?`);
    if (confirm) {
      this.coffees = this.coffees.filter(c => c !== coffee);
      this.coffeeService.deleteCoffee(coffee.id).subscribe();
    }
  }

  sortOnDate(): void {
    console.log('sorting');
    this.coffees.sort((a, b) => new Date(b.time).getTime() - new Date(a.time).getTime());
  }
}
