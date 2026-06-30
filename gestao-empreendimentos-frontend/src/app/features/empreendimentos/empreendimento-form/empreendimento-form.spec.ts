import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpreendimentoForm } from './empreendimento-form';

describe('EmpreendimentoForm', () => {
  let component: EmpreendimentoForm;
  let fixture: ComponentFixture<EmpreendimentoForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmpreendimentoForm],
    }).compileComponents();

    fixture = TestBed.createComponent(EmpreendimentoForm);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
