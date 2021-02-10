import { TestBed } from '@angular/core/testing';

import { RelatedDoctorToCasesService } from "./RelatedDoctorToCasesService";

describe('RelatedDoctorToCasesService', () => {
  let service: RelatedDoctorToCasesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RelatedDoctorToCasesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
