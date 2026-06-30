using System.ComponentModel.DataAnnotations;

namespace GestaoEmpreendimentos.Application.DTOs.Requests
{
    public class CreateEmpreendimentoRequest
    {
        [Required]
        [MinLength(3)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(14)]
        public string Cnpj { get; set; } = string.Empty;

        public string? Endereco { get; set; }
    }
}