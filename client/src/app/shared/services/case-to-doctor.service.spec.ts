import { TestBed } from '@angular/core/testing';

import { CaseToDoctorService } from './case-to-doctor.service';

describe('CaseToDoctorService', () => {
  let service: CaseToDoctorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CaseToDoctorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
