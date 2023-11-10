import { Component, OnInit } from '@angular/core';
import { RecordsService } from '../../records.service';
import { IRecords } from '../../irecords';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  records: IRecords[] = [];

  constructor(public recordsService: RecordsService) { }

  ngOnInit(): void {
    this.recordsService.getRecords().subscribe(records => this.records = records);
  };

  deleteRecord(id: number) {
    this.recordsService.deleteRecord(id).subscribe(res => {
      this.records = this.records.filter(item => item.id !== id);
      console.log('Record deleted successfully');
    });
  };
}
