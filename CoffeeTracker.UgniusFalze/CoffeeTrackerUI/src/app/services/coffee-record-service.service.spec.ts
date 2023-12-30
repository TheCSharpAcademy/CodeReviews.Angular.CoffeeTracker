import { TestBed } from '@angular/core/testing';

import { CoffeeRecordServiceService } from './coffee-record-service.service';

describe('CoffeeRecordServiceService', () => {
  let service: CoffeeRecordServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CoffeeRecordServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
