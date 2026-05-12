import { Routes } from '@angular/router';
import { CafeListComponent } from './components/cafe-list.component/cafe-list.component';
import { CafeFormComponent } from './components/cafe-form/cafe-form';
export const routes: Routes = [
  { path: '', redirectTo: 'list', pathMatch: 'full', title: 'Home Page' },
  { path: 'list', component: CafeListComponent },
  { path: 'add', component: CafeFormComponent },
  { path: 'edit/:id', component: CafeFormComponent },
  { path: '**', redirectTo: 'list' }
];
