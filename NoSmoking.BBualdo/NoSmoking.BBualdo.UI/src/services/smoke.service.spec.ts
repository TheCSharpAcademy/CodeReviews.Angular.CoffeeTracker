import { TestBed } from '@angular/core/testing';

import { SmokeService } from './smoke.service';

describe('SmokeService', () => {
  let service: SmokeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SmokeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
