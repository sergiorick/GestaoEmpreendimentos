using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoEmpreendimentos.Domain.Entities
{
    public class Empreendimento
    {
        public Guid Id { get; private set; }

        [Required]
        [MinLength(3, ErrorMessage = "O nome deve possuir pelo menos 3 caracteres.")]
        public string Nome { get; private set; } = string.Empty;

        [Required]
        public string Cnpj { get; private set; } = string.Empty;

        public string? Endereco { get; private set; }

        [Required]
        public bool Status { get; private set; }

        public DateTime DataCriacao { get; private set; }

        protected Empreendimento() { }

        public Empreendimento(string nome, string cnpj, string? endereco)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Cnpj = cnpj;
            Endereco = endereco;
            Status = true;
            DataCriacao = DateTime.UtcNow;
        }

        public void Inativar()
        {
            Status = false;
        }   
        public void AtualizarDados(string nome, string? endereco)
        {
            if (!Status)
                throw new InvalidOperationException("Empreendimentos inativos não podem ser editados.");
            Nome = nome;
            Endereco = endereco;
        }
    }
}