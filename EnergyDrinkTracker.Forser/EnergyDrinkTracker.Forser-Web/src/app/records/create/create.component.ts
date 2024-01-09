import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DatePipe } from '@angular/common';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';
import { RecordsService } from 'src/app/records.service';
import { Record } from 'src/app/records.modal';
@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css'],
})
export class CreateComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    private recordService: RecordsService,
    public datepipe: DatePipe,
    private router: Router
  ) {}

  myDate = new Date();

  record: Record = {
    drinkDate: this.myDate.toISOString(),
    energyDrink: '',
    canSize: 0,
  };
  submitted = false;

  createRecordForm: FormGroup = new FormGroup({
    drinkdate: new FormControl(''),
    energydrink: new FormControl(''),
    cansize: new FormControl(''),
  });

  ngOnInit(): void {
    this.createRecordForm = this.formBuilder.group({
      drinkDate: ['', Validators.required],
      energyDrink: ['', [Validators.required, Validators.minLength(4)]],
      canSize: ['', Validators.required],
    });
  }

  get f(): { [key: string]: AbstractControl } {
    return this.createRecordForm.controls;
  }

  onFormSubmit(): void {
    if (this.createRecordForm.invalid) {
      return;
    }

    const formDate = this.datepipe.transform(
      this.createRecordForm.controls['drinkDate'].value,
      'yyyy-MM-dd'
    )!;

    const data = {
      drinkDate: new Date(formDate).toISOString(),
      energyDrink: this.createRecordForm.controls['energyDrink'].value,
      canSize: this.createRecordForm.controls['canSize'].value,
    };

    this.recordService.addRecord(data).subscribe({
      next: (res) => {
        console.log(res);
        this.submitted = true;
        this.router.navigateByUrl('records/index');
      },
      error: (e) => console.log(e),
    });
  }

  onReset(): void {
    this.submitted = false;
    this.createRecordForm.reset();
  }
}
