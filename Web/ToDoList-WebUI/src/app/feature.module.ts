import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ListItemsComponent } from './features/to-do-list/components/list-items/smart/list-items.component';
import { ListItemDetailComponent } from './features/to-do-list/components/list-item-detail/smart/list-item-detail.component';
import { AppRoutingModule } from './app-routing.module';
import { ListItemsDisplayComponent } from './features/to-do-list/components/list-items/display/list-items-display.component';
import { ListItemDetailDisplayComponent } from './features/to-do-list/components/list-item-detail/display/list-item-detail-display.component';
import { ListItemsErrorDisplayComponent } from './features/to-do-list/components/list-items/display/list-items-error-display/list-items-error-display.component';
import { ListItemDetailErrorDisplayComponent } from './features/to-do-list/components/list-item-detail/display/list-item-detail-error-display/list-item-detail-error-display.component';




@NgModule({
  declarations: [
    ListItemsComponent,
    ListItemsDisplayComponent,
    ListItemDetailComponent,
    ListItemDetailDisplayComponent,
    ListItemsErrorDisplayComponent,
    ListItemDetailErrorDisplayComponent,
  ],
  imports: [
    CommonModule,
    AppRoutingModule
  ]
})
export class FeatureModule { }
