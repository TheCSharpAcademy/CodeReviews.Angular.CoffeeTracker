import { Component, EventEmitter, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CoffeeTrackerHttpService } from '../../../Services/coffee-tracker-http.service';

@Component({
  selector: 'app-record-modal',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './record-modal.component.html',
  styleUrl: './record-modal.component.css'
})
export class RecordModalComponent {
  coffeeQuantity = 0;

  @Output() addedItem = new EventEmitter<any>();

  constructor(private recordsHttpService: CoffeeTrackerHttpService) { }

  addItem() {
    let date = new Date();
    let currentMonth = date.getMonth() + 1;

    console.log(date.getDate().toString().length);

    let month = date.getMonth().toString().length == 1 ? "0" + currentMonth : date.getMonth();
    let day = date.getDate().toString().length == 1 ? "0" + date.getDate() : date.getDate();

    this.addedItem.emit({
      quantity: this.coffeeQuantity,
      date: [date.getFullYear(), month, day].join('-')
    });
  }
}
