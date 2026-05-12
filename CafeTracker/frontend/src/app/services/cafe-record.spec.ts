import { TestBed } from '@angular/core/testing';

  import { CafeRecord } from '../models/cafe-record.model';

describe('CafeRecord', () => {
  let service: CafeRecord;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CafeRecord);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
