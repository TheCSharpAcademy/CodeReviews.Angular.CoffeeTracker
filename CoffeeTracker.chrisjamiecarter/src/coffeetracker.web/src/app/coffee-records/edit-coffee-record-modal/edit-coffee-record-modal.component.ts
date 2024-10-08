import { CommonModule } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
} from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CoffeeRecord } from '../../shared/coffee-record.interface';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-coffee-record-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-coffee-record-modal.component.html',
  styleUrl: './edit-coffee-record-modal.component.css',
})
export class EditCoffeeRecordModalComponent implements OnChanges {
  @Input() coffeeRecord: CoffeeRecord | null = null;
  @Output() confirmEdit = new EventEmitter<CoffeeRecord>();
  @Output() closeModal = new EventEmitter<void>();

  editCoffeeRecordForm = this.resetEditCoffeeRecordForm();
  formSubmitted: boolean = false;

  constructor(private toastr: ToastrService) {}

  ngOnChanges(changes: SimpleChanges) {
    if (changes['coffeeRecord'] && this.coffeeRecord) {
      this.editCoffeeRecordForm = this.resetEditCoffeeRecordForm();
    }
  }

  onConfirm() {
    this.formSubmitted = true;
    if (this.editCoffeeRecordForm.valid) {
      const request: CoffeeRecord = {
        id: this.coffeeRecord!.id,
        name: this.editCoffeeRecordForm.value.name ?? '',
        date: new Date(this.editCoffeeRecordForm.value.date ?? ''),
      };
      this.confirmEdit.emit(request);
    } else {
      this.showWarningToastr('Form contains invalid fields!');
    }
  }

  onCancel() {
    this.formSubmitted = false;
    this.coffeeRecord = null;
    this.closeModal.emit();
  }

  resetEditCoffeeRecordForm() {
    this.formSubmitted = false;

    if (this.coffeeRecord) {
      const date = this.coffeeRecord.date.toString().split('T')[0];
      return new FormGroup({
        name: new FormControl(this.coffeeRecord.name),
        date: new FormControl(date),
      });
    } else {
      return new FormGroup({
        name: new FormControl(''),
        date: new FormControl(''),
      });
    }
  }

  showErrorToastr(message: string) {
    this.toastr.error(message, 'Error');
  }

  showSuccessToastr(message: string) {
    this.toastr.success(message, 'Success');
  }

  showWarningToastr(message: string) {
    this.toastr.warning(message, 'Warning');
  }
}
