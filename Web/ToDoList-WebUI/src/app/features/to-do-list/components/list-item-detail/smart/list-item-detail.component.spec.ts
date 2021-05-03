import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { of, throwError } from 'rxjs';
import { DataError } from '../../../models/data-error';
import { ListItem } from '../../../models/list-item';
import { ListItemService } from '../../../services/list-item.service';
import { Location } from '@angular/common';

import { ListItemDetailComponent } from './list-item-detail.component';

describe('ListItemDetailComponent', () => {
    let fixture: ComponentFixture<ListItemDetailComponent>
    let component: ListItemDetailComponent;
    let listItemServiceSpy: { getListItem: jasmine.Spy };
    let locationSpy: { back: jasmine.Spy }
    let route: ActivatedRoute;

    beforeEach(() => {
        listItemServiceSpy = jasmine.createSpyObj('listItemService', ['getListItem']);
        locationSpy = jasmine.createSpyObj('location', ['back']);

        TestBed.configureTestingModule({
            declarations: [ListItemDetailComponent],
            providers: [
                ListItemDetailComponent,
                { provide: ListItemService, useValue: listItemServiceSpy },
                { provide: Location, useValue: locationSpy }
            ],
            imports: [RouterTestingModule.withRoutes([])]
        });

        fixture = TestBed.createComponent(ListItemDetailComponent);
        component = fixture.componentInstance;
        listItemServiceSpy = TestBed.inject(ListItemService) as jasmine.SpyObj<ListItemService>;
        locationSpy = TestBed.inject(Location) as jasmine.SpyObj<Location>;
        route = TestBed.inject(ActivatedRoute)
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    describe('ngOnInit', () => {

        it('should call getListItem', () => {
            component.getListItem = jasmine.createSpy("getListItem");

            component.ngOnInit();

            expect(component.getListItem).toHaveBeenCalled();
        })
    })

    describe('getListItem', () => {

        it('should set listItem property to the expected ListItem', () => {
            const expectedListItem: ListItem = {
                id: 1,
                value: 'test1',
                completed: false,
                date: '2021-1-1'
            };

            spyOn(route.snapshot.paramMap, 'get').and.returnValue('1')

            listItemServiceSpy.getListItem.and.returnValue(of(expectedListItem));

            component.getListItem();

            component.listItem$.subscribe(
                listItem => expect(listItem).toEqual(expectedListItem),
                error => fail("Didn't expect an error")
            );
        });

        it('should set dataError property to the expected DataError', () => {
            const expectedError: DataError = {
                status: 404,
                message: 'Not found',
                friendlyMessage: ''
            };

            spyOn(route.snapshot.paramMap, 'get').and.returnValue('1')

            listItemServiceSpy.getListItem.and.returnValue(throwError(expectedError));

            component.getListItem();

            component.listItem$.subscribe(
                listItem => fail("Expected an error"),
                error => { 
                    expect(component.dataError.status).toEqual(expectedError.status);
                    expect(component.dataError.friendlyMessage).toEqual("Can't find list item");
                }
            );
        });
    })

    describe('goBack', () => {

        it('should call location.back', () => {
            component.goBack();
            expect(locationSpy.back).toHaveBeenCalled();
        })
    })

    describe('the dom', () => {

        // it('should only display Loading... when loading', () => {
        //     listItemServiceSpy.getListItem.and.returnValue(of(false));

        //     fixture.detectChanges();

        //     let pEl: HTMLElement = fixture.nativeElement.querySelector('p');
        //     let listItemEl: HTMLElement = fixture.nativeElement.querySelector('app-list-item-detail-display');
        //     let errorEl: HTMLElement = fixture.nativeElement.querySelector('app-list-item-detail-error-display');

        //     expect(pEl.textContent).toContain('Loading...');
        //     expect(listItemEl).toBeFalsy();
        //     expect(errorEl).toBeFalsy();
        // });

    //     it('should only display ListItemDetailDisplayComponnet when when getListItem returns a listItem', () => {
    //         const expectedListItem: ListItem = {
    //             id: 1,
    //             value: 'test1',
    //             completed: false,
    //             date: '2021-1-1'
    //         }

    //         listItemServiceSpy.getListItem.and.returnValue(of(expectedListItem));

    //         fixture.detectChanges();

    //         let pEl: HTMLElement = fixture.nativeElement.querySelector('p');
    //         let listItemEl: HTMLElement = fixture.nativeElement.querySelector('app-list-item-detail-display');
    //         let errorEl: HTMLElement = fixture.nativeElement.querySelector('app-list-item-detail-error-display');

    //         expect(pEl).toBeFalsy();
    //         expect(listItemEl).toBeTruthy();
    //         expect(errorEl).toBeFalsy();
    //     });

    //     it('should only display ListItemDetailErrorDisplayComponnet when when getListItem returns DataError', () => {
    //         const expectedError: DataError = {
    //             status: 404,
    //             message: 'Not found',
    //             friendlyMessage: 'test error'
    //         };

    //         listItemServiceSpy.getListItem.and.returnValue(throwError(expectedError));

    //         fixture.detectChanges();

    //         let pEl: HTMLElement = fixture.nativeElement.querySelector('p');
    //         let listItemEl: HTMLElement = fixture.nativeElement.querySelector('app-list-item-detail-display');
    //         let errorEl: HTMLElement = fixture.nativeElement.querySelector('app-list-item-detail-error-display');

    //         expect(pEl).toBeFalsy();
    //         expect(listItemEl).toBeFalsy();
    //         expect(errorEl).toBeTruthy();
    //     });
    })
});
