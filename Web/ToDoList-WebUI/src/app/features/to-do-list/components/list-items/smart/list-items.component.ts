import { Component, OnInit } from '@angular/core';
import { ListItemService } from '../../../services/list-item.service';
import { ListItem } from '../../../models/list-item';
import { DataError } from '../../../models/data-error';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Component({
  selector: 'app-list-items',
  templateUrl: './list-items.component.html',
})
export class ListItemsComponent implements OnInit {

  listItems$: Observable<ListItem[]>;
  dataError: DataError;

  constructor(private listItemService: ListItemService) { }

  ngOnInit(): void {
    this.getListItems();
  }

  getListItems(): void {
    this.listItems$ = this.listItemService.getList().pipe(
      catchError( (err: DataError) => {
        this.dataError = err;
        console.error(err.friendlyMessage)
        return throwError(err);  // Do we want to rethrow the error here? return of(null)?
      })
    ) as Observable<ListItem[]>;
  }
}
