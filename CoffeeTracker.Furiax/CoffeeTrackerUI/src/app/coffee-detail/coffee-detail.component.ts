import { Component } from '@angular/core';
import { Coffee } from '../coffee';
import { ActivatedRoute } from '@angular/router';
import { CoffeeService } from '../coffeeService';
import { Observable, catchError } from 'rxjs';
import { Location, DatePipe } from '@angular/common';

@Component({
  selector: 'app-coffee-detail',
  templateUrl: './coffee-detail.component.html',
  styleUrl: './coffee-detail.component.css'
})
export class CoffeeDetailComponent {
  coffee: Coffee | undefined;
  constructor(private route: ActivatedRoute,
    private coffeeService: CoffeeService,
    private location: Location,
    private datePipe: DatePipe,
  ) { }

  ngOnInit(): void {
    this.getCoffee();
  }
  getCoffee(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.coffeeService.getCoffee(id)
      .subscribe(coffee => this.coffee = coffee);
  }
  save(): void {
    if (this.coffee) {
      const formattedDate = this.datePipe.transform(this.coffee.time, 'dd/MM/yy');
      this.coffee.time = new Date(formattedDate);

      this.coffeeService.updateCoffee(this.coffee)
        .subscribe(() => this.goBack());
    }
  }
  goBack(): void {
    this.location.back();
  }
}
