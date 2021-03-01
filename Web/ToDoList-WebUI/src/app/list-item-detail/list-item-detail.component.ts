import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ListItemService } from '../list-item.service';
import { Location } from '@angular/common';

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
    private listItemService: ListItemService,
    private location: Location) {this.getListItem(); }

  ngOnInit(): void {
    this.getListItem();
  }

  getListItem(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.listItemService.getListItem(id).subscribe(listItem => this.listItem = listItem)
  }

  goBack(): void {
    this.location.back();
  }
}
