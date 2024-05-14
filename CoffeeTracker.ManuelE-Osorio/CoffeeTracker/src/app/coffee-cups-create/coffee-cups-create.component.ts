import { Component, Input, OnInit, input } from '@angular/core';
import { CoffeeCupsService } from '../coffee-cups.service';
import { CoffeeCups, CoffeeCupsForm, CoffeeMeasureUnits, CoffeeMeasureUnitsMapping } from '../coffee-cups';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Location, NgFor, NgIf, formatDate } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-coffee-cups-create',
  standalone: true,
  imports: [ RouterLink, NgIf, ReactiveFormsModule, NgFor ],
  templateUrl: './coffee-cups-create.component.html'
})

export class CoffeeCupsCreateComponent implements OnInit{
  form : FormGroup<CoffeeCupsForm> = new FormGroup<CoffeeCupsForm>({
    id: new FormControl<number>(0, {nonNullable: true, validators: [
      Validators.required, Validators.min(0), Validators.max(2147483647), Validators.pattern("^[0-9]*$")
    ]} ),
    quantity: new FormControl<number>(1, {nonNullable: true, validators: [
      Validators.required, Validators.min(1), Validators.max(2147483647), Validators.pattern("^[0-9]*$")
    ]}),
    measure: new FormControl<number>(1, {nonNullable: true, validators: [
      Validators.required, Validators.min(1), Validators.max(2147483647), Validators.pattern("^[0-9]*$")
    ]}),
    description: new FormControl<string>('Description', {nonNullable: true, validators: [
      Validators.required, Validators.minLength(3), Validators.maxLength(400)
    ]}),
    units: new FormControl<CoffeeMeasureUnits>(CoffeeMeasureUnits.ml, {nonNullable: true, validators: Validators.required}),
    date: new FormControl<string>(formatDate(Date.now(), 'yyyy-MM-dd', 'en'), {nonNullable: true, validators: Validators.required})
  });

  CoffeeCups!: CoffeeCups;
  Units = Object.values(CoffeeMeasureUnits).filter(value => typeof(value) === 'number');;
  UnitsMapping = CoffeeMeasureUnitsMapping


  constructor( 
    private route: ActivatedRoute,
    private coffeeService : CoffeeCupsService,
    private location: Location,
    private router: Router
  ) {}
  
  ngOnInit() {
    this.CoffeeCups = { id: 0, quantity: 0, measure: 0, description: "", units: 0, date: new Date(Date.now())}
  }

  createCoffeeCup() {
    this.coffeeService.postCoffeeCup( this.CoffeeCups ).subscribe( resp => {
      if( resp.status === 201 && resp.body!= null){
        this.router.navigate([`coffeecups/details/${resp.body.id}`])
      }
    })
  }

  setForm(){
    if (this.CoffeeCups != null){
      this.form.controls.id.setValue(this.CoffeeCups.id)
      this.form.controls.quantity.setValue(this.CoffeeCups.quantity)
      this.form.controls.measure.setValue(this.CoffeeCups.measure)
      this.form.controls.description.setValue(this.CoffeeCups.description)
      this.form.controls.units.setValue(this.CoffeeCups.units)
      this.form.controls.date.setValue(formatDate(this.CoffeeCups.date, 'yyyy-MM-dd', 'en'))
    }
  }

  submitForm(){
    if(this.form.valid){
      this.CoffeeCups = Object.assign(this.CoffeeCups, this.form.value);
      this.createCoffeeCup();
    }
  }

  returnToIndex(){

  }

  goBack() {
    this.router.navigate(["coffeecups/"])
    }
}
