import { Component, EventEmitter, Output } from '@angular/core';
import {
  FormControl,
  NgForm,
  FormGroup,
  Validators,
  FormArray,
  FormBuilder,
} from '@angular/forms';
import { Coffee } from 'src/Models/Coffee';
import { CoffeeServiceService } from 'src/app/coffee-service.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css'],
})
export class FormComponent {
  coffeeForm = new FormGroup({
    id: new FormControl('0'),
    date: new FormControl(new Date().toISOString().substring(0, 10)),
    cups: new FormControl('1'),
  });

  @Output() coffeeAdded = new EventEmitter<Coffee>();

  constructor(private CoffeeService: CoffeeServiceService) {}

  onSubmit() {
    console.log(this.coffeeForm.value);

    let coffee: Coffee = {
      id: +this.coffeeForm.value.id!,
      date: new Date(this.coffeeForm.value.date!),
      cups: +this.coffeeForm.value.cups!,
    };

    this.CoffeeService.addCoffee(coffee).subscribe((data) => {
      console.log(data);
      this.coffeeAdded.emit(coffee);
    });
  }
}
