import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { AbstractControl, FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { RecordsService } from 'src/app/records.service';
import { Record } from 'src/app/records.modal';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {
  id!: number;
  record!: Record;
  editRecordForm!: FormGroup;
  submitted = false;

  constructor(
    public recordsService: RecordsService,
    private route: ActivatedRoute,
    private router: Router,
    public datepipe: DatePipe) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['recordsId'];
    this.recordsService.findRecord(this.id).subscribe((data: Record) => {
      this.record = data;
      this.editRecordForm.controls['drinkDate'].setValue(this.record.drinkDate);
      this.editRecordForm.controls['energyDrink'].setValue(this.record.energyDrink);
      this.editRecordForm.controls['canSize'].setValue(this.record.canSize);
    });

    this.editRecordForm = new FormGroup({
      drinkDate: new FormControl(''),
      energyDrink: new FormControl(''),
      canSize: new FormControl('')
    });
  };
  get f() {
    return this.editRecordForm.controls;
  }

  onFormSubmit() {

    const formDate = this.datepipe.transform(this.editRecordForm.controls['drinkDate'].value, 'yyyy-MM-ddTHH:mm:ssZ')!;

    const data = {
      drinkDate: new Date(formDate).toISOString(),
      energyDrink: this.editRecordForm.controls['energyDrink'].value,
      canSize: this.editRecordForm.controls['canSize'].value
    };

    console.log(this.editRecordForm.value);
    this.recordsService.updateRecord(this.id, data).subscribe((res: any) => {
      console.log('Record updated successfully');
      this.submitted = true;
      this.router.navigateByUrl('records/index');
    })
  }
}
