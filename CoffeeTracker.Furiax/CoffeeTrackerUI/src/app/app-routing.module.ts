import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoffeeRecordComponent } from './coffee-record/coffee-record.component';

const routes: Routes = [
  { path: 'coffee-record', component: CoffeeRecordComponent},
  { path: '', redirectTo: 'coffee-record', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
