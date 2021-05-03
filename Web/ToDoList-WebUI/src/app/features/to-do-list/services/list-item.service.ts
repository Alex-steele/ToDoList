import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListItem } from '../models/list-item';
import { environment } from '../../../../environments/environment'
import { catchError } from 'rxjs/operators';
import { DataError } from '../models/data-error';
import { handleHttpError } from 'src/app/functions/handle-http-error';

@Injectable({
  providedIn: 'root'
})
export class ListItemService {
  private apiEndpoint = environment.apiEndpoint;

  constructor(private http: HttpClient) { }

  getList(): Observable<ListItem[] | DataError> {
    return this.http.get<ListItem[]>(this.apiEndpoint)
      .pipe(
        catchError(err => handleHttpError(err))
      );
  }

  getListItem(id: number): Observable<ListItem | DataError> {
    return this.http.get<ListItem>(`${this.apiEndpoint}/${id}`)
      .pipe(
        catchError(err => handleHttpError(err))
      );
  }
}