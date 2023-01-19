import { TestBed } from '@angular/core/testing';

import { MenbersService } from './members.service';

describe('MenbersService', () => {
  let service: MenbersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MenbersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
