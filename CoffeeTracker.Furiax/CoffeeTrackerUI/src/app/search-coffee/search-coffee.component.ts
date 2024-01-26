import { Component } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { Coffee } from '../coffee';
import { CoffeeService } from '../coffeeService';
import { Location } from '@angular/common';

@Component({
  selector: 'app-search-coffee',
  templateUrl: './search-coffee.component.html',
  styleUrl: './search-coffee.component.css'
})
export class SearchCoffeeComponent {
  coffees$!: Observable<Coffee[]>;
  private searchDate = new Subject<Date>();

  constructor(private coffeeService: CoffeeService, private location: Location) { }

  search(searchDate: Date | null): void {
    if (searchDate !== null) {
      this.searchDate.next(searchDate);
    }
  }

  ngOnInit(): void {
    this.coffees$ = this.searchDate.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap((searchDate: Date) => this.coffeeService.searchCoffees(searchDate)),
    );
  }
  goBack(): void {
    this.location.back();
  }

}
