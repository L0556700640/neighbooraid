import { TestBed } from '@angular/core/testing';

import { HelpCallService } from './help-call.service';

describe('HelpCallService', () => {
  let service: HelpCallService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HelpCallService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
