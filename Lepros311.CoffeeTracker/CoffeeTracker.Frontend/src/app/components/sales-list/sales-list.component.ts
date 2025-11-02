import {Component, OnInit} from '@angular/core';
import {CommonModule, DatePipe} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {ApiService, PaginationParams} from '../../services/api.service';
import {SaleDto, CreateSaleDto, UpdateSaleDto} from '../../models/sale.model';
import {SalesFormComponent} from '../sales-form/sales-form.component';
import {ConfirmationModalComponent} from '../confirmation-modal/confirmation-modal.component';
import {FormModalComponent} from '../form-modal/form-modal.component';

@Component({
    selector: 'app-sales-list',
    standalone: true,
    imports: [CommonModule, FormsModule, SalesFormComponent, ConfirmationModalComponent, FormModalComponent, DatePipe],
    template: `
      <div class="sales-list">
        <h2>Sales Records</h2>

        <div class="list-header">
          <div class="date-filter">
            <label for="filterDate">Filter by Date:</label>
            <input
              type="date"
              id="filterDate"
              [(ngModel)]="filterDate"
              (change)="filterByDate()"
              class="form-control"
            />
            <button (click)="clearDateFilter()" class="btn btn-secondary btn-small">Clear Filter</button>
          </div>

          <button class="add-btn" (click)="addSale()">Add New Sale</button>
        </div>

        @if (loading) {
          <div class="loading">Loading sales...</div>
        }

        @if (error) {
          <div class="error">Error: {{error}}</div>
        }

        @if (!loading && !error) {
          <div class="sales">
            @for (sale of sales; track sale.id) {
              <div class="sale-item">
                <div class="sale-info">
                  <h3>{{sale.coffeeName}}</h3>
                  <p>Date: {{sale.dateAndTimeOfSale | date: 'short'}}</p>
                  <p>Total: {{sale.total | currency: 'USD':'symbol':'1.2-2'}}</p>
                </div>
                <div class="sale-actions">
                  <button class="edit-btn" (click)="editSale(sale)">Edit</button>
                  <button class="delete-btn" (click)="confirmDelete(sale)">Delete</button>
                </div>
              </div>
            }
          </div>
        }

        <div class="pagination">
          <button
            class="btn btn-secondary"
            [disabled]="currentPage <= 1"
            (click)="goToPage(currentPage - 1)">
            Previous
          </button>

          <span class="page-info">
            Page {{currentPage}} of {{totalPages}}
          </span>

          <button
            class="btn btn-secondary"
            [disabled]="currentPage >= totalPages"
            (click)="goToPage(currentPage + 1)">
            Next 
          </button>
        </div>

        <app-form-modal
            [isVisible]="showForm"
            [modalTitle]="isEditing ? 'Edit Sale' : 'Add New Sale'"
            (cancel)="onCancelForm()">
            <app-sales-form
                [sale]="selectedSale"
                [isEditing]="isEditing"
                (save)="onSaveSale($event)"
                (cancel)="onCancelForm()">
            </app-sales-form>
        </app-form-modal>
        
        <app-confirmation-modal
          [isVisible]="showDeleteModal"  
          modalTitle="Delete Sale"
          [message]="deleteMessage"  
          (confirm)="deleteSale()"  
          (cancel)="cancelDelete()">
        </app-confirmation-modal>
      </div>
    `,
    styleUrls: ['././sales-list.component.css']
})
export class SalesListComponent implements OnInit {
  sales: SaleDto[] = [];
  loading = false;
  error: string | null = null;
  showForm = false;
  isEditing = false;
  selectedSale: SaleDto = {id: 0, dateAndTimeOfSale: null, total: 0, coffeeName: '', coffeeId: 0};
  currentPage = 1;
  totalPages = 1;
  pageSize = 10;

  // Modal state
  showDeleteModal = false;
  saleToDelete: SaleDto | null = null;
  deleteMessage = '';

  // Date filter
  filterDate: string = '';

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loadSales();
  }

  loadSales(paginationParams?: PaginationParams): void {
    this.loading = true;
    this.error = null;

    const params = paginationParams || {
      page: this.currentPage,
      pageSize: this.pageSize
    };

    this.apiService.getPagedSales(params).subscribe({
      next: (response) => {
        this.sales = response.data;
        this.loading = false;
        this.totalPages = Math.ceil(response.totalRecords / response.pageSize);
      },
      error: (err) => {
        this.error = 'Failed to load sales';
        this.loading = false;
        console.error('Error loading sales: ', err);
      }
    });
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.loadSales();
    }
  }

  filterByDate(): void {
    if (this.filterDate) {
      const paginationParams: PaginationParams = {
        page: 1,
        pageSize: this.pageSize,
        minDateOfSale: this.filterDate,
        maxDateOfSale: this.filterDate
      };

      this.currentPage = 1;
      this.loadSales(paginationParams);
    } else {
      this.loadSales();
    }
  }

  clearDateFilter(): void {
    this.filterDate = '';
    this.loadSales();
  }

  addSale(): void {
    this.selectedSale = {id: 0, dateAndTimeOfSale: null, total: 0, coffeeName: '', coffeeId: 0};
    this.isEditing = false;
    this.showForm = true;
  }

  editSale(sale: SaleDto): void {
    this.selectedSale = {...sale};
    this.isEditing = true;
    this.showForm = true;
  }

  onSaveSale(saleData: CreateSaleDto | UpdateSaleDto): void {
    if (this.isEditing) {
      this.updateSale(saleData as UpdateSaleDto);
    } else {
      this.createSale(saleData as CreateSaleDto);
    }
  }

  createSale(sale: CreateSaleDto): void {
    this.apiService.createSale(sale).subscribe({
      next: () => {
        this.loadSales();
        this.showForm = false;
      },
      error: (err) => {
        this.error = 'Failed to create sale';
        console.error('Error creating sale: ', err);
      }
    });
  }

  updateSale(sale: UpdateSaleDto): void {
    if (this.selectedSale.id) {
      this.apiService.updateSale(this.selectedSale.id, sale).subscribe({
        next: () => {
          this.loadSales();
          this.showForm = false;
        },
        error: (err) => {
          this.error = 'Failed to update sale';
          console.error('Error updating sale: ', err);
        }
      });
    }
  }

  confirmDelete(sale: SaleDto): void {
    this.saleToDelete = sale;
    this.deleteMessage = `Are you sure you want to delete the sale of "${sale.coffeeName}"? This action cannot be undone.`;
    this.showDeleteModal = true;
  }

  deleteSale(): void {
    if (this.saleToDelete) {
      this.apiService.deleteSale(this.saleToDelete.id).subscribe({
        next: () => {
          this.loadSales();
          this.showDeleteModal = false;
          this.saleToDelete = null;
        },
        error: (err) => {
          this.error = 'Failed to delete sale';
          console.error('Error deleting sale: ', err);
          this.showDeleteModal = false;
          this.saleToDelete = null;
        }
      });
    }
  }

  cancelDelete(): void {
    this.showDeleteModal = false;
    this.saleToDelete = null;
  }

  onCancelForm(): void {
    this.showForm = false;
    this.selectedSale = {id: 0, dateAndTimeOfSale: null, total: 0, coffeeName: '', coffeeId: 0};
    this.isEditing = false;
  }
}