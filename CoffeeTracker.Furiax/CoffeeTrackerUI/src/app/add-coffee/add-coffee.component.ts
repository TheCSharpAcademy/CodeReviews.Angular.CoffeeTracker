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
    id: 0,
    blend: '',
    numberOfCups: 1,
    time: new Date()
  };
  constructor(private coffeeService: CoffeeService, private location: Location) { }

  save(): void {
    if (this.coffee) {
      if (this.coffee.time instanceof Date) {
        const timezoneOffSet = this.coffee.time.getTimezoneOffset();
        const offsetCoffeeTime = new Date(this.coffee.time.getTime() - (timezoneOffSet * 60000));
        this.coffee.time = offsetCoffeeTime;
      }
      this.coffeeService.addCoffee(this.coffee)
        .subscribe(() => this.goBack());
    }
  }

  goBack(): void {
    this.location.back();
  }

}
