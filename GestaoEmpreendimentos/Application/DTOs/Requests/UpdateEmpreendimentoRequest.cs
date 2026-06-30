using System.ComponentModel.DataAnnotations;

namespace GestaoEmpreendimentos.Application.DTOs.Requests
{
    public class UpdateEmpreendimentoRequest
    {
        [Required]
        [MinLength(3)]
        public string Nome { get; set; } = string.Empty;

        public string? Endereco { get; set; }
    }
}
