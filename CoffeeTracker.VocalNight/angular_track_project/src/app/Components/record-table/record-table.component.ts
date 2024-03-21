import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CoffeeRecords } from '../../../Model/CoffeeRecords';
import { FormsModule } from '@angular/forms';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-record-table',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './record-table.component.html',
  styleUrl: './record-table.component.css'
})
export class RecordTableComponent implements OnInit {

  @Input() coffeeRecords!: BehaviorSubject<any>;
  filteredRecord: CoffeeRecords[] = [];
  dateFieldStart: Date = new Date();
  dateFieldEnd: Date = new Date();
  dateFieldValid = true;

  @Output() showModal = new EventEmitter<boolean>();
  @Output() deleteClicked = new EventEmitter<number>();

  ngOnInit(): void {
    this.getRecords();
  }

  getRecords() {
    this.coffeeRecords.subscribe({
      next: records => this.filteredRecord = records,
      error: e => console.error("Api error", e)
    });
  }

  onFilter() {
    if (this.dateFieldStart > this.dateFieldEnd) {
      this.dateFieldValid = false;
      return;
    }
    this.dateFieldValid = true;

    this.coffeeRecords.subscribe({
      next: records => {
        console.log(records);
        this.filteredRecord = records.filter((record: { date: Date; }) => record.date >= this.dateFieldStart && record.date <= this.dateFieldEnd)
      },
      error: e => console.error("Api error", e)
    });
  }

  onClear() {
    this.getRecords();
  }

  addRecord() {
    this.showModal.emit(true);
  }

  deleteItem(id: number) {
    this.deleteClicked.emit(id);
  }
}
