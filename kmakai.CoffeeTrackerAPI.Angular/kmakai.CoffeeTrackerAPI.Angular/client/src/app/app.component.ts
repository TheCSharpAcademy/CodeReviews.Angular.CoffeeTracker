import { Component } from '@angular/core';
import { CoffeeServiceService } from './coffee-service.service';
import { Coffee } from 'src/Models/Coffee';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Client';
  coffeeList: Coffee[] = [];

  constructor(private coffeeService: CoffeeServiceService) {
    this.getList();
  }

  getList() {
    this.coffeeService.getCoffees().subscribe((data) => {
      this.coffeeList = data;
      this.sortList();
    });
  }
  onCoffeeAdded(c: Coffee): void {
    this.coffeeService.getCoffees().subscribe((coffees) => {
      this.coffeeList = coffees;
      this.sortList();
    });
  }

  sortList() {
    this.coffeeList.sort((a, b) => {
      return new Date(b.date).getTime() - new Date(a.date).getTime();
    });
  }
}
