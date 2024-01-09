import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './records/index/index.component';
import { CreateComponent } from './records/create/create.component';
import { EditComponent } from './records/edit/edit.component';

const routes: Routes = [
  { path: '', redirectTo: 'records/index', pathMatch: 'full' },
  { path: 'records/index', component: IndexComponent },
  { path: 'records/create', component: CreateComponent },
  { path: 'records/edit/:recordsId', component: EditComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
