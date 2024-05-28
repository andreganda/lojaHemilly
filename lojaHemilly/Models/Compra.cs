using System.ComponentModel.DataAnnotations;

namespace lojaHemilly.Models
{
    public class Compra
    {
        [Key]
        public int CompraID { get; set; }
        public int FornecedorID { get; set; }
        public DateTime DataCompra { get; set; }
        public decimal ValorTotal { get; set; }

        public Fornecedor Fornecedor { get; set; }
        public List<ItemCompra> ItensCompra { get; set; } = new List<ItemCompra>();
    }
}
