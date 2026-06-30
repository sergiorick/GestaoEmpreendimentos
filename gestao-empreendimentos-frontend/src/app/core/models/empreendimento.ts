// Dados que o usuário envia ao cadastrar
export interface CreateEmpreendimentoRequest {
  nome: string;
  cnpj: string;
  endereco?: string;
}

// Dados que o usuário envia ao editar
export interface UpdateEmpreendimentoRequest {
  nome: string;
  endereco?: string;
}

// Dados que a API retorna
export interface EmpreendimentoResponse {
  id: string;
  nome: string;
  cnpj: string;
  endereco?: string;
  status: string; // "Ativo" ou "Inativo"
  dataCriacao: string;
}

//respota paginada listada
export interface PagedResponse<T> {
  itens: T[];
  paginaAtual: number;
  tamanhoPagina: number;
  totalRegistros: number;
  totalPaginas: number;
}
