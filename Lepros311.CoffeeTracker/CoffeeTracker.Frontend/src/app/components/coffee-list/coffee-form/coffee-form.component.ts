import {Component, Input, Output, EventEmitter, OnInit, ViewChild} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, NgForm} from '@angular/forms';
import {CoffeeDto} from '../../../models/coffee.model';

@Component({
    selector: 'app-coffee-form',
    standalone: true,
    imports: [CommonModule, FormsModule],
    template: `
      <div class="coffee-form">     
        <form (ngSubmit)="onSubmit()" #coffeeForm="ngForm">
          <div class="form-group">
            <label for="name">Coffee Name:</label>
            <input
              type="text"
              id="name"
              name="name"
              [(ngModel)]="coffee.name"
              required
              #nameInput="ngModel"
              class="form-control"
            />
            @if (nameInput.invalid && nameInput.touched) {
                <div class="error-message">
                    Coffee name is required
                </div>
            }
          </div>

          <div class="form-group">
            <label for="price">Price:</label>
            <input
              type="string"
              id="price"
              name="price"
              [(ngModel)]="coffee.price"
              required
              min="0"
              step="0.01"
              #priceInput="ngModel"
              class="form-control"
            />
            @if ((priceInput.invalid && priceInput.touched) || (coffee.price <= 0 && priceInput.touched)) {
                <div class="error-message">
                    Price is required and must be greater than 0
                </div>
            }
          </div>

          <div class="form-actions">
            <button type="submit" [disabled]="coffeeForm.invalid || coffee.price <= 0" class="btn btn-primary">
                {{isEditing ? 'Update Coffee' : 'Add Coffee'}}
            </button>
            <button type="button" (click)="onCancel()" class="btn btn-secondary">
                Cancel 
            </button>
          </div>
        </form>
      </div>
    `,
    styleUrls: ['././coffee-form.component.css']
})
export class CoffeeFormComponent implements OnInit {
  @Input() coffee: CoffeeDto = {id: 0, name: '', price: 0};
  @Input() isEditing = false;
  @Output() save = new EventEmitter<CoffeeDto>();
  @Output() cancel = new EventEmitter<void>();
  @ViewChild('coffeeForm') coffeeForm!: NgForm;

  ngOnInit(): void {
    // Create a copy to avoid modifying the original
    this.coffee = {...this.coffee};
  }

  onSubmit(): void {
    if (this.coffee.name && this.coffee.price > 0) {
        this.save.emit(this.coffee);
    }
  }

  onCancel(): void {
    this.coffeeForm.resetForm();
    this.coffee = {id: 0, name: '', price: 0};
    this.cancel.emit();
  }
}