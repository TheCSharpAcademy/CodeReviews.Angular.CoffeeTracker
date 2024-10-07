import { Component, input, output } from '@angular/core';
import { Coffee } from '../coffe/coffee';
import { CoffeComponent } from "../coffe/coffe.component";

@Component({
  selector: 'app-consumption-list',
  standalone: true,
  imports: [CoffeComponent],
  templateUrl: './consumption-list.component.html',
  styleUrl: './consumption-list.component.css',
})
export class ConsumptionListComponent{
  onDelete=output<string>();
  todaysCoffees = input<Coffee[]>();
  

  removeCoffee(coffee: Coffee) {
    this.onDelete.emit(coffee.id);
    
  }
}
