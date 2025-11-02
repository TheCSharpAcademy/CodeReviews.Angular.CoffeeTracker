import {Component, Input, Output, EventEmitter} from '@angular/core';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-form-modal',
  standalone: true,
  imports: [CommonModule],
  template: `
    @if (isVisible) {
      <div class="modal-overlay" (click)="onCancel()">
        <div class="modal-content" (click)="$event.stopPropagation()">
          <div class="modal-header">
            <h3>{{modalTitle}}</h3>
            <button class="close-btn" (click)="onCancel()">&times;</button>
          </div>
          <div class="modal-body">
            <ng-content></ng-content>
          </div>
        </div>
      </div>
    }
  `,
  styleUrls: ['././form-modal.component.css']
})
export class FormModalComponent {
  @Input() isVisible = false;
  @Input() modalTitle = 'Form';
  @Output() cancel = new EventEmitter<void>();

  onCancel(): void {
    this.cancel.emit();
  }
}