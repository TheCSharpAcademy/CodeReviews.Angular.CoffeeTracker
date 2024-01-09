import { Component, Input } from '@angular/core';
import { CoffeeRecord } from '../CoffeeRecord';
import { CoffeeRecordServiceService } from '../services/coffee-record-service.service';


@Component({
  selector: 'app-coffee-records',
  templateUrl: './coffee-records.component.html',
  styleUrl: './coffee-records.component.css'
})
export class CoffeeRecordsComponent {
  coffeeRecords: CoffeeRecord[] = [];
  dates: Date[] = [];
  filterDate?:Date;


  constructor(private coffeeService:CoffeeRecordServiceService){}

  ngOnInit(): void{
    this.getCoffeeRecords();
  }

  getCoffeeRecords(): void {
    this.coffeeService.getCoffeeRecords()
      .subscribe(coffeeRecords => {
        this.coffeeRecords = coffeeRecords;
        for(let i = 0; i<this.coffeeRecords.length; i++){
          let coffeeRecordDate = new Date(this.coffeeRecords[i].recordDate.toISOString());
          coffeeRecordDate.setHours(0, 0, 0, 0);
          if(this.dates.find((date) => date.getTime() === coffeeRecordDate.getTime()) === undefined){
            this.dates.push(coffeeRecordDate);
          }
        }
      });
  }

  delete(coffeeRecord: CoffeeRecord): void{
    this.coffeeRecords = this.coffeeRecords.filter(cR => cR !== coffeeRecord);
    this.coffeeService.deleteCoffeeRecord(coffeeRecord.coffeeRecordId).subscribe();
  }
}
