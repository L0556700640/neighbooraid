import { TestBed } from '@angular/core/testing';

import { KeywordsToCasesService } from './keywords-to-cases.service';

describe('KeywordsToCasesService', () => {
  let service: KeywordsToCasesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(KeywordsToCasesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
