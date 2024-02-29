import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AddCoffeeFormComponent } from "./add-coffee-form/add-coffee-form.component";
import { CoffeeListComponent } from "./coffee-list/coffee-list.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CoffeeListComponent, AddCoffeeFormComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'CoffeeTracker';
}
