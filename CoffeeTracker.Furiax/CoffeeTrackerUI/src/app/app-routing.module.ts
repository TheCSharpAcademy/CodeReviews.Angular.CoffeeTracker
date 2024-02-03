import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoffeeRecordComponent } from './coffee-record/coffee-record.component';
import { CoffeeDetailComponent } from './coffee-detail/coffee-detail.component';
import { AddCoffeeComponent } from './add-coffee/add-coffee.component';
import { SearchCoffeeComponent } from './search-coffee/search-coffee.component';

const routes: Routes = [
  { path: 'coffee-record', component: CoffeeRecordComponent},
  { path: '', redirectTo: 'coffee-record', pathMatch: 'full' },
  { path: 'detail/:id', component: CoffeeDetailComponent },
  { path: 'add', component: AddCoffeeComponent },
  { path: 'search', component: SearchCoffeeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
