import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CoffeeRecordServiceService } from '../services/coffee-record-service.service';
import { CoffeeRecord } from '../CoffeeRecord';
import { Location } from '@angular/common';

@Component({
  selector: 'app-coffee-record-edit',
  templateUrl: './coffee-record-edit.component.html',
  styleUrl: './coffee-record-edit.component.css'
})
export class CoffeeRecordEditComponent {
  constructor(
    private route:ActivatedRoute, 
    private coffeeService:CoffeeRecordServiceService, 
    private location: Location
  ){}
  
  @Input() coffeeRecord:CoffeeRecord = {
    'coffeeRecordId' : 0,
    'cupsOfCoffee' : 0,
    'recordDate': new Date()
  }

  ngOnInit(){
    this.getCoffeeRecord();
  }

  getCoffeeRecord(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if(id !== null){
      this.coffeeService.getCoffeeRecord(Number(id))
        .subscribe(coffeeRecord => this.coffeeRecord = coffeeRecord);
    }
  }

  goBack(): void {
    this.location.back();
  }

  onSubmit():void{
    if(this.coffeeRecord.coffeeRecordId === 0){
      this.coffeeService.insertCoffeeRecord(this.coffeeRecord)
        .subscribe(() => this.goBack());
    }else{
      this.coffeeService.updateCoffeeRecord(this.coffeeRecord, this.coffeeRecord.coffeeRecordId)
        .subscribe({
          next: () => this.goBack() 
        });
    }
  }
}
