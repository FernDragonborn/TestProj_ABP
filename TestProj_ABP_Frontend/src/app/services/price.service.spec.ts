import { TestBed } from '@angular/core/testing';

import { PriceTestService } from './price.service';

describe('PriceService', () => {
  let service: PriceTestService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PriceTestService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
