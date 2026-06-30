import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpreendimentoList } from './empreendimento-list';

describe('EmpreendimentoList', () => {
  let component: EmpreendimentoList;
  let fixture: ComponentFixture<EmpreendimentoList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmpreendimentoList],
    }).compileComponents();

    fixture = TestBed.createComponent(EmpreendimentoList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
