import { Routes } from '@angular/router';
import { MainMenuComponent } from '../components/main-menu/main-menu.component';
import { DashboardComponent } from '../components/dashboard/dashboard.component';

export const routes: Routes = [
  { path: '', component: MainMenuComponent },
  { path: 'dashboard', component: DashboardComponent },
];
