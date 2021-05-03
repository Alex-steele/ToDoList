import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ListItemService } from '../../../services/list-item.service';
import { Location } from '@angular/common';

import { ListItem } from '../../../models/list-item';
import { DataError } from '../../../models/data-error';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-list-item-detail',
  templateUrl: './list-item-detail.component.html',
})
export class ListItemDetailComponent implements OnInit {

  listItem$: Observable<ListItem>;
  dataError: DataError;

  constructor(
    private route: ActivatedRoute,
    private listItemService: ListItemService,
    private location: Location) { }

  ngOnInit(): void {
    this.getListItem();
  }

  getListItem(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.listItem$ = this.listItemService.getListItem(id).pipe(
      catchError((err: DataError) => {
        this.dataError = err;

        if (err.status === 404) {
          this.dataError.friendlyMessage = "Can't find list item"
        }

        console.error(this.dataError.friendlyMessage)
        return throwError(this.dataError); // Do we want to rethrow the error here? return of(null)?
      })
    ) as Observable<ListItem>;
  }

  goBack(): void {
    this.location.back();
  }
}
