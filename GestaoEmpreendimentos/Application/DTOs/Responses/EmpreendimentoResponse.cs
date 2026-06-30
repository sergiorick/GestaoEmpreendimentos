namespace GestaoEmpreendimentos.Application.DTOs.Responses
{
    public class EmpreendimentoResponse
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Cnpj { get; set; } = string.Empty;

        public string? Endereco { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime DataCriacao { get; set; }
    }
}