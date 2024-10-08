import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CoffeeRecordService } from '../../shared/coffee-record.service';
import { CreateCoffeeRecord } from '../../shared/create-coffee-record.interface';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-coffee-record-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-coffee-record-form.component.html',
  styleUrl: './add-coffee-record-form.component.css',
})
export class AddCoffeeRecordFormComponent {
  addCoffeeRecordForm = this.resetAddCoffeeRecordForm();
  formSubmitted: boolean = false;

  constructor(
    private coffeeRecordService: CoffeeRecordService,
    private toastr: ToastrService
  ) {}

  onSubmit() {
    this.formSubmitted = true;
    if (this.addCoffeeRecordForm.valid) {
      const request: CreateCoffeeRecord = {
        name: this.addCoffeeRecordForm.value.name ?? '',
        date: this.addCoffeeRecordForm.value.date ?? '',
      };

      this.coffeeRecordService.addCoffeeRecord(request).subscribe((result) => {
        if (result) {
          this.showSuccessToastr('Coffee recorded successfully!');
        } else {
          this.showErrorToastr('Unable to record Coffee!');
        }
      });

      this.addCoffeeRecordForm = this.resetAddCoffeeRecordForm();
    } else {
      this.showWarningToastr('Form contains invalid fields!');
    }
  }

  resetAddCoffeeRecordForm() {
    this.formSubmitted = false;
    const today = new Date().toISOString().split('T')[0];
    return new FormGroup({
      name: new FormControl(''),
      date: new FormControl(today),
    });
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
