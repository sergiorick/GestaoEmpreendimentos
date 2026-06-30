namespace GestaoEmpreendimentos.Application.DTOs.Responses
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Itens { get; set; } = new List<T>();
        public int PaginaAtual { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
    }
}