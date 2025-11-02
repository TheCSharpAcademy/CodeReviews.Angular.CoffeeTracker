import {Component, Input, Output, EventEmitter, OnInit, ViewChild, OnChanges, SimpleChanges} from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {FormsModule, NgForm} from '@angular/forms';
import {SaleDto, CreateSaleDto, UpdateSaleDto} from '../../models/sale.model';
import {CoffeeDto} from '../../models/coffee.model';
import {ApiService, PaginationParams} from '../../services/api.service';

@Component({
  selector: 'app-sales-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  providers: [DatePipe],
  template: `
    <div class="sales-form">
      <form (ngSubmit)="onSubmit()" #saleForm="ngForm">
        <div class="form-group">
          <label for="coffeeId">Coffee:</label>
          <select
            id="coffeeId"
            name="coffeeId"
            [(ngModel)]="selectedCoffeeId"
            required
            #coffeeInput="ngModel"
            class="form-control"
          >
            <option [ngValue]="null">Select a coffee</option>
            @for (coffee of coffees; track coffee.id) {
              <option [ngValue]="coffee.id">{{coffee.name}} - {{coffee.price | currency:'USD':'symbol':'1.2-2'}}</option>
            }
          </select>
          @if (coffeeInput.invalid && coffeeInput.touched && selectedCoffeeId === null) {
            <div class="error-message">Please select a coffee</div>
          }
        </div>

        <div class="form-group">
          <label for="dateAndTimeOfSale">Date and Time:</label>
          <input
            type="datetime-local"
            id="dateAndTimeOfSale"
            name="dateAndTimeOfSale"
            [ngModel]="dateTimeString"
            (ngModelChange)="onDateChange($event)"
            #dateInput="ngModel"
            class="form-control"
          />
          <small class="form-text">Leave empty to use current date and time</small>
        </div>

        <div class="form-actions">
          <button type="submit" [disabled]="saleForm.invalid || loading || selectedCoffeeId === null" class="btn btn-primary">
            {{isEditing ? 'Update Sale' : 'Add Sale'}}
          </button>
          <button type="button" (click)="onCancel()" class="btn btn-secondary">
            Cancel 
          </button>
        </div>
      </form>
    </div>
  `,
  styleUrls: ['./sales-form.component.css']
})
export class SalesFormComponent implements OnInit, OnChanges {
  @Input() sale: SaleDto = {id: 0, dateAndTimeOfSale: null, total: 0, coffeeName: '', coffeeId: 0};
  @Input() isEditing = false;
  @Output() save = new EventEmitter<CreateSaleDto | UpdateSaleDto>();
  @Output() cancel = new EventEmitter<void>();
  @ViewChild('saleForm') saleForm!: NgForm;

  coffees: CoffeeDto[] = [];
  loading = false;
  selectedCoffeeId: number | null = null; // Add this for the dropdown
  dateTimeString: string = '';

  constructor(private apiService: ApiService, private datePipe: DatePipe) {}

  ngOnInit(): void {
    this.loadCoffees();
    // Create a copy to avoid modifying the original
    this.sale = {...this.sale};

    // Set selectedCoffeeId to null for new sales, or the actual value for editing
    this.selectedCoffeeId = this.isEditing ? this.selectedCoffeeId : null;

    // Convert backend date format to datetime-local format
    if (this.sale.dateAndTimeOfSale) {
      const date = new Date(this.sale.dateAndTimeOfSale);
      this.dateTimeString = this.datePipe.transform(date, 'yyyy-MM-ddTHH:mm') || '';
    } else if (!this.isEditing) {
      // Set default date to now if not editing
      this.sale.dateAndTimeOfSale = new Date();
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['isEditing'] || changes['sale']) {
      this.selectedCoffeeId = this.isEditing && this.sale?.coffeeId ? this.sale.coffeeId : null;
      if (this.sale.dateAndTimeOfSale) {
        const date = new Date(this.sale.dateAndTimeOfSale);
        this.dateTimeString = this.datePipe.transform(date, 'yyyy-MM-ddTHH:mm') || '';
        // Set default date to now if not editing;
      } else if (!this.isEditing) {
        this.dateTimeString = '';
      }
      if (!this.isEditing && this.saleForm) {
        this.saleForm.resetForm();
      }
    }
  }

  onDateChange(dateString: string): void {
    this.dateTimeString = dateString;
    if (dateString) {
      this.sale.dateAndTimeOfSale = new Date(dateString);
    } else {
      this.sale.dateAndTimeOfSale = null;
    }
  }

  loadCoffees(): void {
    this.loading = true;
    const paginationParams: PaginationParams = {
      page: 1,
      pageSize: 100 // Get all coffees
    };

    this.apiService.getPagedCoffees(paginationParams).subscribe({
      next: (response) => {
        this.coffees = response.data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading coffees: ', err);
        this.loading = false;
      }
    });
  }

  onSubmit(): void {
    if (this.selectedCoffeeId && this.selectedCoffeeId > 0) {
      let dateValue: Date;
      
      if (this.dateTimeString) {
        // Parse the datetime-local string (YYYY-MM-DDTHH:mm) and create date in local timezone
        const [datePart, timePart] = this.dateTimeString.split('T');
        const [year, month, day] = datePart.split('-').map(Number);
        const [hours, minutes] = timePart.split(':').map(Number);
        
        // Create date explicitly in local timezone (this avoids timezone interpretation issues)
        dateValue = new Date(year, month - 1, day, hours, minutes);
      } else {
        dateValue = new Date();
      }
      
      const saleData = {
        coffeeId: this.selectedCoffeeId,
        dateAndTimeOfSale: dateValue.toISOString()
      };
      console.log('Sending sale data:', saleData);
      console.log('dateTimeString value:', this.dateTimeString);
      this.save.emit(saleData);
    }
  }

  onCancel(): void {
    this.selectedCoffeeId = null;
    this.sale = {id: 0, dateAndTimeOfSale: null, total: 0, coffeeName: '', coffeeId: 0};
    this.saleForm.resetForm();
    this.cancel.emit();
  }
}