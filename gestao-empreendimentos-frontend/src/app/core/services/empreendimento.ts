import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  CreateEmpreendimentoRequest,
  UpdateEmpreendimentoRequest,
  EmpreendimentoResponse,
  PagedResponse,
} from '../models/empreendimento';

@Injectable({
  providedIn: 'root',
})
export class EmpreendimentoService {
  private readonly apiUrl = `${environment.apiUrl}/empreendimentos`;

  constructor(private http: HttpClient) {}

  create(request: CreateEmpreendimentoRequest): Observable<EmpreendimentoResponse> {
    return this.http.post<EmpreendimentoResponse>(this.apiUrl, request);
  }

  getAll(
    nome?: string,
    status?: string,
    ordenarPor?: string,
    pagina: number = 1,
    tamanhoPagina: number = 10,
  ): Observable<PagedResponse<EmpreendimentoResponse>> {
    let params: any = {
      pagina: pagina.toString(),
      tamanhoPagina: tamanhoPagina.toString(),
    };
    if (nome) params.nome = nome;
    if (status) params.status = status;
    if (ordenarPor) params.ordenarPor = ordenarPor;

    return this.http.get<PagedResponse<EmpreendimentoResponse>>(this.apiUrl, { params });
  }

  getById(id: string): Observable<EmpreendimentoResponse> {
    return this.http.get<EmpreendimentoResponse>(`${this.apiUrl}/${id}`);
  }

  update(id: string, request: UpdateEmpreendimentoRequest): Observable<EmpreendimentoResponse> {
    return this.http.put<EmpreendimentoResponse>(`${this.apiUrl}/${id}`, request);
  }

  inativar(id: string): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}/inativar`, {});
  }
}
