import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CoffeeCupsListComponent } from './coffee-cups-list/coffee-cups-list.component';
import { NotificationsComponent } from './notifications/notifications.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CoffeeCupsListComponent, RouterOutlet, NotificationsComponent],
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'CoffeeTracker';
}
