import { TestBed } from '@angular/core/testing';

import { SearchWordService } from './search-word.service';

describe('SearchWordService', () => {
  let service: SearchWordService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SearchWordService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
