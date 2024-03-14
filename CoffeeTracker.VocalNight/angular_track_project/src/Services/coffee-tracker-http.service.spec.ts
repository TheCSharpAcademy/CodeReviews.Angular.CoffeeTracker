import { TestBed } from '@angular/core/testing';

import { CoffeeTrackerHttpService } from './coffee-tracker-http.service';

describe('CoffeeTrackerHttpService', () => {
  let service: CoffeeTrackerHttpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CoffeeTrackerHttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
