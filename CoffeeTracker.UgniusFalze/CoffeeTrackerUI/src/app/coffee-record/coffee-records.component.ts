import { Component } from '@angular/core';
import { CoffeeRecord } from '../CoffeeRecord';
import { CoffeeRecordServiceService } from '../services/coffee-record-service.service';


@Component({
  selector: 'app-coffee-records',
  templateUrl: './coffee-records.component.html',
  styleUrl: './coffee-records.component.css'
})
export class CoffeeRecordsComponent {
  coffeeRecords: CoffeeRecord[] = [];

  constructor(private coffeeService:CoffeeRecordServiceService){}

  ngOnInit(): void{
    this.getCoffeeRecords();
  }

  getCoffeeRecords(): void {
    this.coffeeService.getCoffeeRecords()
      .subscribe(coffeeRecords => this.coffeeRecords = coffeeRecords);
  }
}
