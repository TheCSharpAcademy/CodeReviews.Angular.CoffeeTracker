import { Routes } from '@angular/router';
import { CoffeeRecordsComponent } from './coffee-records/coffee-records.component';

export const routes: Routes = [
  {
    path: '',
    component: CoffeeRecordsComponent,
    title: 'Coffee Tracker - Home',
  },
];
