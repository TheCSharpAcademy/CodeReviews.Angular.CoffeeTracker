import { Component } from '@angular/core';
import { FormsModule } from "@angular/forms";
import { Coffee } from "../coffee.model";
import { CoffeeService } from "../coffee.service";
import { Location } from "@angular/common";

@Component({
  selector: 'app-add-coffee-form',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './add-coffee-form.component.html',
  styleUrl: './add-coffee-form.component.css'
})
export class AddCoffeeFormComponent {
  coffee: Coffee = {
    id: 0,
    time: new Date(),
    coffeeType: ''
  }

  constructor(private coffeeService: CoffeeService, private location: Location) {
  }
  onSubmit() {
    this.coffeeService.addCoffee(this.coffee)
      .subscribe();
    window.location.reload();

  }
}
