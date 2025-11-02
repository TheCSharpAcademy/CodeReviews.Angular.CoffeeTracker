import {Component} from '@angular/core';
import {RouterOutlet, RouterLink} from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-root',
  template:`
    <main>
      <nav class="navbar">
        <a [routerLink]="['/']" class="nav-brand"><h1>☕ Coffee Tracker</h1></a>
        <div class="nav-links">
          <a [routerLink]="['/']" class="nav-link">Coffees</a>
          <a [routerLink]="['/sales']" class="nav-link">Sales</a>
        </div>
      </nav>
      <section class="content">
        <router-outlet></router-outlet>
      </section>
    </main>
  `,
  styleUrls: ['./app.css'],
  imports: [RouterOutlet, RouterLink],
})
export class App {
  title = 'Coffee Tracker';
}
