import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-confirmation-modal',
  standalone: true,
  imports: [CommonModule],
  template: `
    @if (isVisible) {
      <div class="modal-overlay" (click)="onCancel()">
        <div class="modal-content" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <h3>{{modalTitle}}</h3>
          </div>
          <div class="modal-body">
            <p>{{message}}</p>
          </div>
          <div class="modal-footer">
            <button class="btn btn-secondary" (click)="onCancel()">Cancel</button>
            <button class="btn btn-danger" (click)="onConfirm()">Confirm</button>
          </div>
        </div>
      </div>
    }
  `,
  styleUrls: ['././confirmation-modal.component.css']
})
export class ConfirmationModalComponent {
  @Input() isVisible = false;
  @Input() modalTitle = 'Confirm Action';
  @Input() message = 'Are you sure you want to proceed?';
  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  onConfirm(): void {
    this.confirm.emit();
  }

  onCancel(): void {
    this.cancel.emit();
  }
}