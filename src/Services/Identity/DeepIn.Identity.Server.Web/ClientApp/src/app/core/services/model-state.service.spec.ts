/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ModelStateService } from './model-state.service';

describe('Service: ModelState', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ModelStateService]
    });
  });

  it('should ...', inject([ModelStateService], (service: ModelStateService) => {
    expect(service).toBeTruthy();
  }));
});
