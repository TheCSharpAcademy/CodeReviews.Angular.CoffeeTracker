import { Pipe, PipeTransform } from '@angular/core';
import { CoffeeRecord } from './CoffeeRecord';

@Pipe({
  name: 'tableFilter'
})
export class TableFilterPipe implements PipeTransform {

  transform(coffeeRecords: CoffeeRecord[], dateFilter?:Date):CoffeeRecord[]  {
    if(dateFilter == undefined) return coffeeRecords;
    return coffeeRecords.filter((coffeeRecord) => {
      let coffeeRecordDate = new Date(coffeeRecord.recordDate.toISOString());
      coffeeRecordDate.setHours(0, 0, 0, 0);
      return coffeeRecordDate.getTime() === dateFilter.getTime()
    });
  }

}
