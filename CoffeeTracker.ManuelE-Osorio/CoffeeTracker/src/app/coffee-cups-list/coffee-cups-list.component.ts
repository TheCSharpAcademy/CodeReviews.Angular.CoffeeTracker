import { Component, OnInit } from '@angular/core';
import { CoffeeCupsService } from '../coffee-cups.service';
import { Subject, debounceTime, distinctUntilChanged, map, switchMap } from 'rxjs';
import { CoffeeCups } from '../coffee-cups';
import { NgFor, NgIf } from '@angular/common';
import { CoffeeMeasureUnits } from '../coffee-cups';
import { formatDate } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-coffee-cups-list',
  standalone: true,
  imports: [NgFor, NgIf, RouterLink, ReactiveFormsModule],
  templateUrl: './coffee-cups-list.component.html'
})

export class CoffeeCupsListComponent implements OnInit{

  coffeeCups : CoffeeCups[] | undefined = []
  form : FormGroup = new FormGroup({
    date: new FormControl<string | null >( null, {})
  });

  private searchDate = new Subject<string>();


  constructor (
    private coffeeService : CoffeeCupsService
  ) {}

  ngOnInit() : void {
    this.getCups()

    this.searchDate.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap((term: string) => this.coffeeService.getCoffeeCups(term)),
    ).subscribe( resp =>
      this.coffeeCups = resp.body?.map( cups => {
        return {id: cups.id, description : cups.description,
          date : new Date(cups.date),
          measure : cups.measure,
          quantity : cups.quantity,
          units : cups.units}
      }));
  }

  getCups( date?: string)
  {
    this.coffeeService.getCoffeeCups(date).subscribe( resp =>
      this.coffeeCups = resp.body?.map( cups => {
        return {id: cups.id, description : cups.description,
          date : new Date(cups.date),
          measure : cups.measure,
          quantity : cups.quantity,
          units : cups.units}
      })
    );
  }

  search(term: string ): void {
    this.searchDate.next(term);
  }

  getEnumString( units: CoffeeMeasureUnits) : string {
    return CoffeeMeasureUnits[units]
  }
}
