using System.ComponentModel.DataAnnotations;

namespace lojaHemilly.Models
{
    public class Fornecedor
    {
        [Key]
        public int FornecedorID { get; set; }
        public string NomeFornecedor { get; set; }
        public string Contato { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public List<Compra> Compras { get; set; } = new List<Compra>();
    }
}
