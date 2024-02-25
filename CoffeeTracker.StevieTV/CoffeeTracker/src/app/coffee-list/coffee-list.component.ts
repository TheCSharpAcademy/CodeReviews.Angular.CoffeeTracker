import { DatePipe, NgForOf, NgIf } from "@angular/common";
import { Component, OnInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AddCoffeeFormComponent } from "../add-coffee-form/add-coffee-form.component";
import { Coffee } from "../coffee.model";
import { CoffeeService } from "../coffee.service";

@Component({
  selector: 'app-coffee-list',
  standalone: true,
  imports: [
    NgForOf,
    DatePipe,
    AddCoffeeFormComponent,
    FormsModule,
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './coffee-list.component.html',
  styleUrl: './coffee-list.component.css'
})
export class CoffeeListComponent implements OnInit {
  coffees: Coffee[] = [];
  filtered = false;
  constructor(private coffeeService: CoffeeService) {
  }

  ngOnInit(): void {
    this.getCoffees();
  }
  getCoffees() {
    this.coffeeService.getCoffees()
      .subscribe(coffees => this.coffees = coffees);
    this.filtered = false;
  }

  filterCoffees(searchDate: Date){
    this.coffeeService.filterCoffeeByDate(searchDate)
      .subscribe(coffees => this.coffees = coffees)
    this.filtered = true;
  }

  deleteCoffee(coffee: Coffee) {
    this.coffees = this.coffees.filter(c => c.id !== coffee.id);
    this.coffeeService.deleteCoffee(coffee.id).subscribe();
  }
}
