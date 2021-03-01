import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ListItemService } from '../list-item.service';

import { listItem } from '../listItem';

@Component({
  selector: 'app-list-item-detail',
  templateUrl: './list-item-detail.component.html',
  styleUrls: ['./list-item-detail.component.css']
})
export class ListItemDetailComponent implements OnInit {

  listItem: listItem;

  constructor(
    private route: ActivatedRoute,
    private listItemService: ListItemService) { }

  ngOnInit(): void {
  }

  getListItem(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.listItemService
  }

}
