import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of, throwError } from 'rxjs';
import { DataError } from '../../../models/data-error';
import { ListItem } from '../../../models/list-item';
import { ListItemService } from '../../../services/list-item.service';
import { ListItemsDisplayComponent } from '../display/list-items-display.component';
import { ListItemsErrorDisplayComponent } from '../display/list-items-error-display/list-items-error-display.component';

import { ListItemsComponent } from './list-items.component';

describe('ListItemsComponent', () => {

    let fixture: ComponentFixture<ListItemsComponent>;
    let component: ListItemsComponent;
    let listItemServiceSpy: { getList: jasmine.Spy };

    beforeEach(() => {
        listItemServiceSpy = jasmine.createSpyObj('listItemService', ['getList']);

        TestBed.configureTestingModule({
            declarations: [
                ListItemsComponent, 
                ListItemsDisplayComponent, 
                ListItemsErrorDisplayComponent
            ],
            providers: [
                ListItemsComponent,
                { provide: ListItemService, useValue: listItemServiceSpy },
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(ListItemsComponent);
        component = fixture.componentInstance;
        listItemServiceSpy = TestBed.inject(ListItemService) as jasmine.SpyObj<ListItemService>;
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    describe('ngOnInit', () => {

        it('should call getListItem', () => {
            component.getListItems = jasmine.createSpy("getListItems");

            component.ngOnInit();

            expect(component.getListItems).toHaveBeenCalled();
        })
    })

    describe('getListItem', () => {

        it('should get the expected list', () => {
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

            listItemServiceSpy.getList.and.returnValue(of(expectedListItems));

            component.getListItems();

            component.listItems$.subscribe(
                listItems => expect(listItems).toEqual(expectedListItems),
                error => fail("Didn't expect an error")
            );
        });

        it('should return DataError when the server returns an error', () => {
            const expectedError: DataError = {
                status: 404,
                message: 'Not found',
                friendlyMessage: 'test error'
            };

            listItemServiceSpy.getList.and.returnValue(throwError(expectedError));

            component.getListItems();

            component.listItems$.subscribe(
                listItems => fail("Expected an error"),
                error => expect(component.dataError).toEqual(expectedError)
            );
        });
    });

    describe('the dom', () => {

        
        it('should only display Loading... when loading', () => {
            listItemServiceSpy.getList.and.returnValue(of(false));
            
            fixture.detectChanges();
            
            let loadingEl: HTMLElement = fixture.nativeElement.querySelector('#loading');
            let listItemsEl: HTMLElement = fixture.nativeElement.querySelector('app-list-items-display');
            let errorEl: HTMLElement = fixture.nativeElement.querySelector('app-list-items-error-display');

            expect(loadingEl).toBeTruthy();
            expect(listItemsEl).toBeFalsy();
            expect(errorEl).toBeFalsy();
        });

        it('should only display ListItemsDisplayComponet when getList returns listItems', () => {
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

            listItemServiceSpy.getList.and.returnValue(of(expectedListItems));
            
            fixture.detectChanges();
            
            let loadingEl: HTMLElement = fixture.nativeElement.querySelector('#loading');
            let listItemsEl: HTMLElement = fixture.nativeElement.querySelector('app-list-items-display');
            let errorEl: HTMLElement = fixture.nativeElement.querySelector('app-list-items-error-display');

            expect(loadingEl).toBeFalsy();
            expect(listItemsEl).toBeTruthy();
            expect(errorEl).toBeFalsy();
        });

        // it('should only display ListItemsErrorComponet when getList returns DataError', () => {
        //     const expectedError: DataError = {
        //         status: 404,
        //         message: 'Not found',
        //         friendlyMessage: 'test error'
        //     };

        //     listItemServiceSpy.getList.and.returnValue(throwError(expectedError));
            
        //     fixture.detectChanges();
            
        //     let loadingEl: HTMLElement = fixture.nativeElement.querySelector('#loading');
        //     let listItemsEl: HTMLElement = fixture.nativeElement.querySelector('app-list-items-display');
        //     let errorEl: HTMLElement = fixture.nativeElement.querySelector('app-list-items-error-display');

        //     expect(loadingEl).toBeFalsy();
        //     expect(listItemsEl).toBeFalsy();
        //     expect(errorEl).toBeTruthy();
        // });
    });
});
