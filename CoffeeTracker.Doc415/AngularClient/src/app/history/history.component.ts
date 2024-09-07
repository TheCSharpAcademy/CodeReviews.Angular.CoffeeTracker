import { HttpClient } from '@angular/common/http';
import { Component,output,inject,signal} from '@angular/core';
import { Coffee } from '../coffe/coffee';
import { CoffeComponent } from "../coffe/coffe.component";
import { CoffeeService } from '../coffee.service';

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [CoffeComponent],
  templateUrl: './history.component.html',
  styleUrl: './history.component.css'
})
export class HistoryComponent {
  private httpClient=inject(HttpClient);
  private _coffeeService=inject(CoffeeService)

 onClose=output<boolean>();
selectedDate='';
coffeesOnSelectedDate=signal<Coffee[]>([]);

onDateChange(event:any){
  this.selectedDate=event.target.value;
  console.log(this.selectedDate)
  this._coffeeService.getCoffeesAtDate(this.selectedDate).subscribe({
    next: resData => this.coffeesOnSelectedDate.set([...resData]),
    error: ()=> this.coffeesOnSelectedDate.set([])
  })
}

closeDialog() {
  this.onClose.emit(false);
}
}
