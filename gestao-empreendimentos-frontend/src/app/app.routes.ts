import { Routes } from '@angular/router';
import { EmpreendimentoList } from './features/empreendimentos/empreendimento-list/empreendimento-list';
import { EmpreendimentoForm } from './features/empreendimentos/empreendimento-form/empreendimento-form';

export const routes: Routes = [
  { path: '', redirectTo: 'empreendimentos', pathMatch: 'full' },
  { path: 'empreendimentos', component: EmpreendimentoList },
  { path: 'empreendimentos/novo', component: EmpreendimentoForm },
  { path: 'empreendimentos/editar/:id', component: EmpreendimentoForm },
];
