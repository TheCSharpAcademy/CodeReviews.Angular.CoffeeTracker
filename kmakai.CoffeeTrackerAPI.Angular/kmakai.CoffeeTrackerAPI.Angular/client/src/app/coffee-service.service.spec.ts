import { TestBed } from '@angular/core/testing';

import { CoffeeServiceService } from './coffee-service.service';

describe('CoffeeServiceService', () => {
  let service: CoffeeServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CoffeeServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
