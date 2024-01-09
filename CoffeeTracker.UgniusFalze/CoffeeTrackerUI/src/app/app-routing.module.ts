import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { CoffeeRecordEditComponent } from './coffee-record-edit/coffee-record-edit.component';
import { CoffeeRecordsComponent } from './coffee-record/coffee-records.component';

const routes: Routes = [
  { path: '', redirectTo: '/records', pathMatch: 'full' },
  {path: 'add', component:CoffeeRecordEditComponent},
  {path:'records', component:CoffeeRecordsComponent},
  {path: 'update/:id', component:CoffeeRecordEditComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
