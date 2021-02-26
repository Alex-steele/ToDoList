import { Component, OnInit } from '@angular/core';
import { ListItemService } from '../list-item.service';
import { listItem } from '../listItem';

@Component({
  selector: 'app-list-items',
  templateUrl: './list-items.component.html',
  styleUrls: ['./list-items.component.css']
})
export class ListItemsComponent implements OnInit {

  listItems: listItem[];

  constructor(private listItemService: ListItemService) {}

  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
    this.listItemService.getList()
      .subscribe(listItems => this.listItems = listItems)
  }

}
