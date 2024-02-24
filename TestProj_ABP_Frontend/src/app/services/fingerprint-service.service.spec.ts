import { TestBed } from '@angular/core/testing';

import { FingerprintService } from './fingerprint-service.service';

describe('FingerprintServiceService', () => {
  let service: FingerprintService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FingerprintService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
