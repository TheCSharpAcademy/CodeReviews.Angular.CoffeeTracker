import { Component,inject,signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./header/header.component";
import { CoffeComponent } from "./coffe/coffe.component";
import { coffeList } from './coffe-data';
import { Coffee } from './coffe/coffee';
import { ConsumptionListComponent } from "./consumption-list/consumption-list.component";
import { HistoryComponent } from "./history/history.component";
import { HttpClient } from '@angular/common/http';
import { SpinnerComponent } from "./spinner/spinner.component";


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, CoffeComponent, ConsumptionListComponent, HistoryComponent, SpinnerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent {
  private httpClient=inject(HttpClient);
  isLoading=true;
  title = 'coffeeTracker';
  coffeeList=coffeList;
  todaysCoffees=signal<Coffee[]>([]);
  isHistoryOpen=false;

  async ngOnInit(){   
      await this.getCoffees();   
   }

  onDeleteCoffee(coffeeId:string){
    this.httpClient.delete(`https://localhost:7273/api/Coffees/${coffeeId}`).subscribe({
      next: resData => this.todaysCoffees.set(this.todaysCoffees().filter(coffee=> coffee.id!==coffeeId)),
      error: err=> console.log(err)
    }
   
    );
  }

  onSelectCoffee(coffeeSelected:Coffee){
      this.httpClient.post<Coffee>("https://localhost:7273/api/Coffees",coffeeSelected).subscribe({
        next: resData => this.todaysCoffees.set([...this.todaysCoffees(),resData]),
        error: err=> console.log(err)
      }
       
      );
          
    
    console.log(this.todaysCoffees())
  }

  async getCoffees(){
    const currentDate =  new Date().toISOString().split('T')[0];
    const formattedDate = encodeURIComponent(currentDate);
    
      this.httpClient.get<Coffee[]>(`https://localhost:7273/api/Coffees/${formattedDate}`).subscribe({
        next: resData => {
                          this.todaysCoffees.set([...resData]);
                          this.isLoading=false;
        },
        error: async(err)=> {
          await this.getCoffees();
          console.log("Waiting server")
        }

      })    
  }

  toggleHistoryDialog(){
    this.isHistoryOpen=true;
  }

  closeHistoryDialog(isOpen:boolean){
    this.isHistoryOpen=isOpen;
  }
}
