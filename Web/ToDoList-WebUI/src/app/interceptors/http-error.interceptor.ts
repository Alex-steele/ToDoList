// import {
//   HttpEvent,
//   HttpInterceptor,
//   HttpHandler,
//   HttpRequest,
//   HttpErrorResponse
//  } from '@angular/common/http';
//  import { Observable, throwError } from 'rxjs';
//  import { retry, catchError } from 'rxjs/operators';
 
//  export class HttpErrorInterceptor implements HttpInterceptor {
//   intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
//     return next.handle(request)
//       .pipe(
//         retry(1),
//         catchError((error: HttpErrorResponse) => {
//           let errorMessage = '';

//           if (error.error instanceof ErrorEvent) {
//             errorMessage = `Error: ${error.error.message}`;
//           } else {
//             errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
//           }
          
//           window.alert(errorMessage);
//           console.error(errorMessage);
//           return throwError(errorMessage);
//         })
//       )
//   }
//  }
