import { Component, Input } from '@angular/core';
import { DataError } from 'src/app/features/to-do-list/models/data-error';

@Component({
  selector: 'app-list-items-error-display',
  templateUrl: './list-items-error-display.component.html',
  styleUrls: ['../list-items.component.css']
})
export class ListItemsErrorDisplayComponent {
  @Input() error: DataError;
}
