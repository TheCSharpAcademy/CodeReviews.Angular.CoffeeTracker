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
  selector: 'app-add-modal',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './add-modal.component.html',
  styleUrl: './add-modal.component.css',
})
export class AddModalComponent {
  @Input() isOpen: boolean = false;
  @Output() closeEvent = new EventEmitter<boolean>();

  addFormGroup = new FormGroup({
    date: new FormControl<string | null>(null),
    quantity: new FormControl<number>(1),
  });

  maxDate = formatDate(new Date(), 'yyyy-MM-ddTHH:mm', 'en-US');

  constructor(private smokeService: SmokeService) {}

  addForm() {
    if (
      this.addFormGroup.value.date == null ||
      Number(this.addFormGroup.value.quantity) < 1
    ) {
      return;
    }

    const log: SmokeLog = {
      date: new Date(this.addFormGroup.value.date).toISOString(),
      quantity: Number(this.addFormGroup.value.quantity),
    };

    this.smokeService.addLog(log).subscribe({
      next: () => {
        this.addFormGroup.reset();
        this.closeModal();
      },
    });
  }

  closeModal() {
    this.closeEvent.emit(false);
  }
}
