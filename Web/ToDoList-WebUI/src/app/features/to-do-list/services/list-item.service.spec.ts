import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { defer } from 'rxjs';

import { ListItem } from '../models/list-item';
import { ListItemService } from './list-item.service';

export function asyncError<T>(errorObject: T) {
  return defer(() => Promise.reject(errorObject));
}

export function asyncData<T>(data: T) {
  return defer(() => Promise.resolve(data));
}

describe('ListItemService', () => {
  let service: ListItemService;
  let httpClientSpy: { get: jasmine.Spy };

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get']);
    TestBed.configureTestingModule({
      providers: [
        ListItemService,
        { provide: HttpClient, useValue: httpClientSpy }
      ]
    });
    service = TestBed.inject(ListItemService);
    httpClientSpy = TestBed.inject(HttpClient) as jasmine.SpyObj<HttpClient>;
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getList', () => {

    it('should return expected list', () => {
      const expectedListItems: ListItem[] = [
        {
          id: 1,
          value: 'test1',
          completed: false,
          date: '2021-1-1'
        },
        {
          id: 2,
          value: 'test2',
          completed: true,
          date: '2021-2-2'
        }
      ]

      httpClientSpy.get.and.returnValue(asyncData(expectedListItems));

      service.getList().subscribe(
        listItems => expect(listItems).toEqual(expectedListItems),
        fail
      );
      expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
    });

    it('should throw a DataError when the server returns an error', () => {
      const errorResponse = new HttpErrorResponse({
        error: 'test 404 error',
        status: 404,
        statusText: 'Not found'
      });

      httpClientSpy.get.and.returnValue(asyncError(errorResponse));

      service.getList().subscribe(
        listItems => fail("expected an error"),
        dataError => {
          expect(dataError.status).toEqual(errorResponse.status);
          expect(dataError.message).toEqual(errorResponse.statusText)
        }
      )
      expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
    });

    describe('getListItem', () => {

      it('should return expected list item', () => {
        const expectedListItem: ListItem = {
          id: 1,
          value: 'test1',
          completed: false,
          date: '2021-1-1'
        };

        httpClientSpy.get.and.returnValue(asyncData(expectedListItem));

        service.getListItem(1).subscribe(
          listItem => expect(listItem).toEqual(expectedListItem),
          fail
        );
        expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
      });

      it('should throw a DataError when the server returns an error', () => {
        const errorResponse = new HttpErrorResponse({
          error: 'test 404 error',
          status: 404,
          statusText: 'Not found'
        });

        httpClientSpy.get.and.returnValue(asyncError(errorResponse));

        service.getListItem(1).subscribe(
          listItem => fail('expected an error'),
          dataError => {
            expect(dataError.status).toEqual(errorResponse.status);
            expect(dataError.message).toEqual(errorResponse.statusText)
          }
        );
        expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
      });
    });
  });
});
