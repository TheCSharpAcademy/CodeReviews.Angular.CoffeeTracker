import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CafeRecord } from '../../models/cafe-record.model';
import { CafeRecordService } from '../../services/cafe-record';
import { ProductCategoryOptions } from '../../product-category';

@Component({
  selector: 'app-cafe-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './cafe-list.component.html',
  styleUrl: './cafe-list.component.css',
})
export class CafeListComponent {
  private cafeRecordService = inject(CafeRecordService);

  cafeRecords = signal<CafeRecord[]>([]);
  filterDate = signal('');
  filterProductName = signal('');
  filterCategory = signal(0);
  errorMessage = signal('');
  productCategories = ProductCategoryOptions;

  constructor() {
    this.loadRecords();
  }

  loadRecords(): void {
    this.cafeRecordService
      .getAll(this.filterProductName() || undefined, this.filterDate() || undefined, this.filterCategory() || undefined)
      .subscribe({
        next: (data) => this.cafeRecords.set(data),
        error: () => this.errorMessage.set('Failed to load cafe records.')
      });
  }

  onSearch(): void {
    this.loadRecords();
  }

  onClearFilters(): void {
    this.filterProductName.set('');
    this.filterDate.set('');
    this.filterCategory.set(0);
    this.loadRecords();
  }


deleteRecord(id: number): void {
    if (!confirm('Are you sure you want to delete this record?')) return;
    this.cafeRecordService.delete(id).subscribe({
      next: () => {
        this.cafeRecords.update(records => records.filter(r => r.id !== id));
      },
      error: () => this.errorMessage.set('Failed to delete the record.')
    });
  }

}