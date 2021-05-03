import { Component, Input, Output, EventEmitter } from '@angular/core';
import { DataError } from 'src/app/features/to-do-list/models/data-error';

@Component({
  selector: 'app-list-item-detail-error-display',
  templateUrl: './list-item-detail-error-display.component.html',
  styleUrls: ['../list-item-detail.component.css']
})
export class ListItemDetailErrorDisplayComponent {

  @Input() error: DataError;
  @Output() goBackClick = new EventEmitter();

  goBack(): void {
    this.goBackClick.emit();
  }
}