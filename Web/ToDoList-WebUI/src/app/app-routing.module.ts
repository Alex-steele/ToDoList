import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListItemDetailComponent } from './features/to-do-list/components/list-item-detail/smart/list-item-detail.component';
import { ListItemsComponent } from './features/to-do-list/components/list-items/smart/list-items.component';

const routes: Routes = [
  { path: 'todolist', component: ListItemsComponent },
  { path: '', redirectTo: '/todolist', pathMatch: 'full' },
  { path: 'details/:id', component: ListItemDetailComponent },
  { path: '**', redirectTo: '/todolist', pathMatch: 'full' }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
