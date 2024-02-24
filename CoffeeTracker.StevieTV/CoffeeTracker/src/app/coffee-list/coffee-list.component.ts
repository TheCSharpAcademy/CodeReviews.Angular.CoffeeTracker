import { DatePipe, NgForOf } from "@angular/common";
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
    ReactiveFormsModule
  ],
  templateUrl: './coffee-list.component.html',
  styleUrl: './coffee-list.component.css'
})
export class CoffeeListComponent implements OnInit {
  coffees: Coffee[] = [];
  constructor(private coffeeService: CoffeeService) {
  }

  ngOnInit(): void {
    debugger;
    this.getCoffees();
  }
  getCoffees() {
    debugger;
    this.coffeeService.getCoffees()
      .subscribe(coffees => this.coffees = coffees);
  }
  // addCoffee(date: Date, coffeeType: string) {
  //   coffeeType = coffeeType.trim();
  //   if (!date) { return; }
  //   this.coffeeService.addCoffee({ time: date, coffeeType: coffeeType } as Coffee)
  //     .subscribe((coffee: Coffee) => this.coffees.push(coffee));
  //
  // }

  filterCoffees(searchDate: Date){
    this.getCoffees();
    if (searchDate != null) {
      searchDate.setHours(0,0,0);

    this.coffees = this.coffees.filter(c => {
        // console.log(new Date(c.time).getTime() === searchDate.getTime());
        return new Date(c.time).getTime() === searchDate.getTime();
      }
    );
    }
  }

  deleteCoffee(coffee: Coffee) {
    this.coffees = this.coffees.filter(c => c.id !== coffee.id);
    this.coffeeService.deleteCoffee(coffee.id).subscribe();
  }
}
