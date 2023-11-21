import { Component, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { Coffee } from 'src/Models/Coffee';
import { CoffeeServiceService } from 'src/app/coffee-service.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})
export class ListComponent {
  @Input() coffees: Coffee[] = [];
  constructor(private CoffeeService: CoffeeServiceService) {}

  deleteCoffee(id: number) {
    this.CoffeeService.deleteCoffee(id).subscribe((data) => {
      console.log(data);
      this.CoffeeService.getCoffees().subscribe((data) => {
        this.coffees = data;
      });
    });
  }
}
