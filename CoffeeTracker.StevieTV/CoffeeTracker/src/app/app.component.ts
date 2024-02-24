import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CoffeeListComponent } from "./coffee-list/coffee-list.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CoffeeListComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'CoffeeTracker';
}
