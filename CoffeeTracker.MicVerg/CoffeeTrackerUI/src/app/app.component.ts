import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CoffeeComponent } from './coffee/coffee.component';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CoffeeComponent, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'CoffeeTrackerUI';
}
