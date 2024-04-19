import { Routes } from '@angular/router';
import { CoffeeCupsListComponent } from './coffee-cups-list/coffee-cups-list.component';
import { CoffeeCupsDetailsComponent } from './coffee-cups-details/coffee-cups-details.component';
import { CoffeeCupsCreateComponent } from './coffee-cups-create/coffee-cups-create.component';

export const routes: Routes = [
    { path: 'coffeecups', component: CoffeeCupsListComponent},
    { path: 'coffeecups/details/:id', component: CoffeeCupsDetailsComponent },
    { path: 'coffeecups/create', component: CoffeeCupsCreateComponent},
    { path: '', redirectTo: '/coffeecups', pathMatch: 'full' }
];
