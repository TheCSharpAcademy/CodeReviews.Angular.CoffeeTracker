import { Component, computed,signal,input,output } from '@angular/core';
import { Coffee } from './coffee';

@Component({
  selector: 'app-coffe',
  standalone: true,
  imports: [],
  templateUrl: './coffe.component.html',
  styleUrl: './coffe.component.css'
})

export class CoffeComponent {
   coffee=input.required<Coffee>();
   onSelect=output<Coffee>();
   isInTodaysList=input<boolean>();
   isInHistoryList=input<boolean>();

   imagePath=computed(()=>"../../assets/coffees/"+this.coffee().avatar)

   onSelectCoffee(){
    this.onSelect.emit(this.coffee())
   }

}
