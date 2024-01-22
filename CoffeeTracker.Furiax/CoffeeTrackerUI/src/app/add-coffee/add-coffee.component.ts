import { Component } from '@angular/core';
import { Coffee } from '../coffee';
import { CoffeeService } from '../coffeeService';
import { Location } from '@angular/common';


@Component({
  selector: 'app-add-coffee',
  templateUrl: './add-coffee.component.html',
  styleUrl: './add-coffee.component.css'
})
export class AddCoffeeComponent {
  coffee: Coffee = {
    blend: '',
    numberOfCups: 0,
    time: new Date()
  };
  constructor(private coffeeService: CoffeeService, private location: Location) { }

  save(): void {
    if (this.coffee) {
      this.coffeeService.addCoffee(this.coffee)
        .subscribe(() => this.goBack());
    }
  }

  goBack(): void {
    this.location.back();
  }
}
