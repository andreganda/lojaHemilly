using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace lojaHemilly.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteID { get; set; }

        [Required (ErrorMessage = "Nome é obrigatório")]
        public string? Nome { get; set; }

		public string? Endereco { get; set; }

		public string? Cpf { get; set; }
		public string? Rg { get; set; }
		public DateTime? DataNascimento { get; set; }
		public string? EstadoCivil { get; set; }
		public string? ContatoConjuge { get; set; }
		public string? CasaPropriaAluguel { get; set; }
		public string? Profissao { get; set; }
		public string? LocalTrabalho { get; set; }
		public decimal? RendaMensal{ get; set; }


		[EmailAddress(ErrorMessage = "Esse não é um email válido")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Telefone { get; set; }

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataAlteracaoCadastro { get; set; }

        public ICollection<Venda>? Vendas { get; set; }

        [DefaultValue(1)]
        public int Status { get; set; }
    }
}
