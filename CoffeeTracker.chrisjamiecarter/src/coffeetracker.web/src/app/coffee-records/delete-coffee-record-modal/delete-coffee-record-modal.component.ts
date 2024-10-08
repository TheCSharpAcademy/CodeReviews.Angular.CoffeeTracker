import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CoffeeRecord } from '../../shared/coffee-record.interface';

@Component({
  selector: 'app-delete-coffee-record-modal',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './delete-coffee-record-modal.component.html',
  styleUrl: './delete-coffee-record-modal.component.css',
})
export class DeleteCoffeeRecordModalComponent {
  @Input() coffeeRecord: CoffeeRecord | null = null;
  @Output() confirmDelete = new EventEmitter<string>();
  @Output() closeModal = new EventEmitter<void>();

  onConfirm() {
    this.confirmDelete.emit(this.coffeeRecord!.id);
  }

  onCancel() {
    this.closeModal.emit();
  }
}
