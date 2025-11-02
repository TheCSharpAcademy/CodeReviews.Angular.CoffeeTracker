import {Component, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ApiService, PaginationParams} from '../../services/api.service';
import {CoffeeDto} from '../../models/coffee.model';
import {CoffeeFormComponent} from './coffee-form/coffee-form.component'
import { ConfirmationModalComponent } from '../confirmation-modal/confirmation-modal.component';
import {FormModalComponent} from '../form-modal/form-modal.component';

@Component({
    selector: 'app-coffee-list',
    standalone: true,
    imports: [CommonModule, CoffeeFormComponent, ConfirmationModalComponent, FormModalComponent],
    template: `
        <div class="coffee-list">
            <h2>Coffee List</h2>

            <div class="list-header">
              <div></div> <!-- Empty div to push Add button to the right -->
              <button class="add-btn" (click)="addCoffee()">Add New Coffee</button>
            </div>

            @if (loading) {
                <div class="loading">Loading coffees...</div>
            }

            @if (error) {
                <div class="error">Error: {{error}}</div>
            }

            @if (!loading && !error) {
                <div class="coffees">
                    @for (coffee of coffees; track coffee.id) {
                        <div class="coffee-item">
                            <div class="coffee-info">
                              <h3>{{coffee.name}}</h3>
                              <p>Price: {{coffee.price | currency: 'USD':'symbol':'1.2-2'}}</p>
                            </div>
                            <div class="coffee-actions">
                              <button class="edit-btn" (click)="editCoffee(coffee)">Edit</button>
                              <button class="delete-btn" (click)="confirmDelete(coffee)">Delete</button>
                            </div>
                        </div>
                    }
                </div>

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
            }

            <app-form-modal
                [isVisible]="showForm"
                [modalTitle]="isEditing ? 'Edit Coffee' : 'Add New Coffee'"
                (cancel)="onCancelForm()">
                <app-coffee-form
                    [coffee]="selectedCoffee"
                    [isEditing]="isEditing"
                    (save)="onSaveCoffee($event)"
                    (cancel)="onCancelForm()">
                </app-coffee-form>
            </app-form-modal>

            <app-confirmation-modal
                [isVisible]="showDeleteModal"
                modalTitle="Delete Coffee"
                [message]="deleteMessage"
                (confirm)="deleteCoffee()"
                (cancel)="cancelDelete()">
            </app-confirmation-modal>
        </div>
    `,
    styleUrls: ['./coffee-list.component.css']
})
export class CoffeeListComponent implements OnInit {
    coffees: CoffeeDto[] = [];
    loading = false;
    error: string | null = null;
    showForm = false;
    isEditing = false;
    selectedCoffee: CoffeeDto = {id: 0, name: '', price: 0};

    showDeleteModal = false;
    coffeeToDelete: CoffeeDto | null = null;
    deleteMessage = '';

    // Pagination state
currentPage = 1;
totalPages = 1;
pageSize = 10;

    constructor(private apiService: ApiService) {}

    ngOnInit(): void {
        this.loadCoffees();
    }

    loadCoffees(): void {
        this.loading = true;
        this.error = null;

        const paginationParams: PaginationParams = {
            page: this.currentPage,
            pageSize: this.pageSize
        };

        this.apiService.getPagedCoffees(paginationParams).subscribe({
            next: (response) => {
                this.coffees = response.data;
                this.totalPages = Math.ceil(response.totalRecords / response.pageSize);
                this.loading = false;
            },
            error: (err) => {
                this.error = 'Failed to load coffees';
                this.loading = false;
                console.error('Error loading coffees: ', err);
            }
        });
    }

    goToPage(page: number): void {
      if (page >= 1 && page <= this.totalPages) {
        this.currentPage = page;
        this.loadCoffees();
      }
    }

    addCoffee(): void {
        this.selectedCoffee = {id: 0, name: '', price: 0};
        this.isEditing = false;
        this.showForm = true;
    }

    editCoffee(coffee: CoffeeDto): void {
        this.selectedCoffee = {...coffee};
        this.isEditing = true;
        this.showForm = true;
    }

    onSaveCoffee(coffee: CoffeeDto): void {
        if (this.isEditing) {
            this.updateCoffee(coffee);
        } else {
            this.createCoffee(coffee);
        }
    }

    createCoffee(coffee: CoffeeDto): void {
        this.apiService.createCoffee(coffee).subscribe({
            next: () => {
                this.loadCoffees();
                this.showForm = false;
            },
            error: (err) => {
                this.error = 'Failed to create coffee';
                console.error('Error creating coffee: ', err);
            }
        });
    }

    updateCoffee(coffee: CoffeeDto): void {
        this.apiService.updateCoffee(coffee.id, coffee).subscribe({
            next: () => {
                this.loadCoffees();
                this.showForm = false;
            },
            error: (err) => {
                this.error = 'Failed to update coffee';
                console.error('Error updating coffee: ', err);
            }
        });
    }

    confirmDelete(coffee: CoffeeDto): void {
        this.coffeeToDelete = coffee;
        this.deleteMessage = `Are you sure you want to delete "${coffee.name}"? This action cannot be undown.`;
        this.showDeleteModal = true;
    }

    deleteCoffee(): void {
        if (this.coffeeToDelete) {
            this.apiService.deleteCoffee(this.coffeeToDelete.id).subscribe({
                next: () => {
                    this.loadCoffees();
                    this.showDeleteModal = false;
                    this.coffeeToDelete = null;
                },
                error: (err) => {
                    this.error = 'Failed to delete coffee';
                    console.error('Error deleting coffee: ', err);
                    this.showDeleteModal = false;
                    this.coffeeToDelete = null;
                }
            });
        }
    }

    cancelDelete(): void {
        this.showDeleteModal = false;
        this.coffeeToDelete = null;
    }

    onCancelForm(): void {
      this.showForm = false;
      this.selectedCoffee = {id: 0, name: '', price: 0};
      this.isEditing = false;
    }
}