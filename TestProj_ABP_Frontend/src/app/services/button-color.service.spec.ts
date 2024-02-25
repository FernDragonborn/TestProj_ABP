import { TestBed } from '@angular/core/testing';

import { ButtonColorService } from './button-color.service';

describe('CuttonColorService', () => {
  let service: ButtonColorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ButtonColorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
