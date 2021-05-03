import { Component, Input } from '@angular/core';
import { ListItem } from '../../../models/list-item';

@Component({
  selector: 'app-list-items-display',
  templateUrl: './list-items-display.component.html',
  styleUrls: ['./list-items.component.css']
})
export class ListItemsDisplayComponent {
    @Input() listItems: ListItem[] = [];
}