import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListItemDetailComponent } from '../list-item-detail/list-item-detail.component';
import { ListItemsComponent } from '../list-items/list-items.component';

const routes: Routes = [
  { path: 'todolist', component: ListItemsComponent},
  { path: '', redirectTo: '/todolist', pathMatch: 'full'},
  { path: 'details/:id', component: ListItemDetailComponent}
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
