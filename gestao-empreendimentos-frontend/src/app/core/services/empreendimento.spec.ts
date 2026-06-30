import { TestBed } from '@angular/core/testing';

import { Empreendimento } from './empreendimento';

describe('Empreendimento', () => {
  let service: Empreendimento;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Empreendimento);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
