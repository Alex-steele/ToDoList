import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { listItem } from './listItem';

@Injectable({
  providedIn: 'root'
})
export class ListItemService {
  private listItemUrl = "https://localhost:44321/api/ToDoList/";

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getList():Observable<listItem[]> {
    return this.http.get<listItem[]>(this.listItemUrl)
      .pipe(catchError(this.handleError<listItem[]>('getList', [])))
  }

  getListItem(id: number):Observable<listItem> {
    return this.http.get<listItem>(`${this.listItemUrl}/${id}`)
      .pipe(catchError(this.handleError<listItem>('getListItem')))
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error:any): Observable<T> => {
      console.error(error);
      return of(result as T);
    }

  }
}
