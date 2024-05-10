import { formatDate } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { SmokeService } from '../../services/smoke.service';
import { SmokeLog } from '../../models/SmokeLog';

@Component({
  selector: 'app-edit-modal',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './edit-modal.component.html',
  styleUrl: './edit-modal.component.css',
})
export class EditModalComponent {
  @Input() isOpen: boolean = false;
  @Input() logToUpdate: SmokeLog | null = null;
  @Output() closeEvent = new EventEmitter<boolean>();

  editFormGroup = new FormGroup({
    date: new FormControl<string | null>(this.logToUpdate?.date || null),
    quantity: new FormControl<number>(this.logToUpdate?.quantity || 1),
  });

  maxDate = formatDate(new Date(), 'yyyy-MM-ddTHH:mm', 'en-US');

  constructor(private smokeService: SmokeService) {}

  addForm() {
    if (
      this.editFormGroup.value.date == null ||
      Number(this.editFormGroup.value.quantity) < 1
    ) {
      return;
    }

    const log: SmokeLog = {
      id: this.logToUpdate!.id,
      date: new Date(this.editFormGroup.value.date).toISOString(),
      quantity: Number(this.editFormGroup.value.quantity),
    };

    this.smokeService.updateLog(this.logToUpdate!.id!, log).subscribe({
      next: () => {
        this.editFormGroup.reset();
        this.closeModal();
      },
    });
  }

  closeModal() {
    this.closeEvent.emit(false);
  }
}
