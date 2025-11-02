import {Routes} from '@angular/router';
import {CoffeeListComponent} from './components/coffee-list/coffee-list.component';
import {SalesListComponent} from './components/sales-list/sales-list.component';

export const routes: Routes = [
    {path: '', component: CoffeeListComponent},
    {path: 'coffees', component: CoffeeListComponent},
    {path: 'sales', component: SalesListComponent}
];
