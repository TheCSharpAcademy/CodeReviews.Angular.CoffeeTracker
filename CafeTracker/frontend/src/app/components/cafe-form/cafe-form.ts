import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CafeRecordService } from '../../services/cafe-record';
import { CafeRecord } from '../../models/cafe-record.model';
import { ProductCategory, ProductCategoryOptions } from '../../product-category';

@Component({
  selector: 'app-cafe-form',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './cafe-form.html',
  styleUrl: './cafe-form.css',
})
export class CafeFormComponent {
  private cafeRecordService = inject(CafeRecordService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  isEditMode = signal(false);
  recordId = signal<number | null>(null);
  errorMessage = signal('');

  cafeModel = signal({
    productName: '',
    category: 0,
    quantity: 1,
    dateConsumed: '',
    notes: ''
  });

  productCategories = ProductCategoryOptions;

  private resolveCategoryValue(category: string): number {
    const match = this.productCategories.find(
      (option) => option.label === category || ProductCategory[option.value] === category
    );

    return match?.value ?? ProductCategory.None;
  }

  constructor() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode.set(true);
      this.recordId.set(+id);
      this.loadRecord(+id);
    }
  }

  loadRecord(id: number): void {
    this.cafeRecordService.getById(id).subscribe({
      next: (record) => {
        this.cafeModel.set({
          productName: record.productName,
          category: this.resolveCategoryValue(record.category as string),
          quantity: record.quantity,
          dateConsumed: new Date(record.dateConsumed + 'Z').toISOString().substring(0, 10), 
          notes: record.notes ?? '',
        });
      },
      error: () => this.errorMessage.set('Failed to load the record.')
    });
  }

  onSubmit(event: Event): void {
    event.preventDefault();

    const model = this.cafeModel();

    // manual validation
    if (!model.productName || model.category === 0 || !model.dateConsumed) {
      this.errorMessage.set('Please fill in all required fields.');
      return;
    }

    const formValue = this.cafeModel() as unknown as CafeRecord;

    if (this.isEditMode()) {
      this.cafeRecordService.update(this.recordId()!, formValue).subscribe({
        next: () => this.router.navigate(['/list']),
        error: () => this.errorMessage.set('Failed to update the record.')
      });
    } else {
      this.cafeRecordService.create(formValue).subscribe({
        next: () => this.router.navigate(['/list']),
        error: () => this.errorMessage.set('Failed to create the record.')
      });
    }
  }

  onCancel(): void {
    this.router.navigate(['/list']);
  }
}