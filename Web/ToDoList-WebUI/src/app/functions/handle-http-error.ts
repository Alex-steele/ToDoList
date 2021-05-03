import { HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { DataError } from '../features/to-do-list/models/data-error';

export const handleHttpError = (error: HttpErrorResponse): Observable<DataError> => {
    let dataError = new DataError(
        error.status,
        error.statusText,
        "An error occured while requesting the data."
    );

    return throwError(dataError)
}