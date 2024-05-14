import { TestBed } from '@angular/core/testing';

import { CoffeeCupsService } from './coffee-cups.service';

describe('CoffeeCupsService', () => {
  let service: CoffeeCupsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CoffeeCupsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
