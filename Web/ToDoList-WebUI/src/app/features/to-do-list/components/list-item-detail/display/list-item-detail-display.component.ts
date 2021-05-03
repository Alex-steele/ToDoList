import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ListItem } from '../../../models/list-item';

@Component({
  selector: 'app-list-item-detail-display',
  templateUrl: './list-item-detail-display.component.html',
  styleUrls: ['./list-item-detail.component.css']
})
export class ListItemDetailDisplayComponent{

  @Input() listItem: ListItem;
  @Output() goBackClick = new EventEmitter();

  goBack(): void {
    this.goBackClick.emit();
  }
}