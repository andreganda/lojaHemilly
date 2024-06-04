using System.ComponentModel.DataAnnotations.Schema;

namespace lojaHemilly.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DataDaVenda { get; set; }
        public int ClienteId { get; set; }
        public decimal PrecoTotal { get; set; }
        public int NumeroParcelas { get; set; }
        public Cliente Cliente { get; set; }
        public decimal Total { get; set; }
        public ICollection<ItemVenda> Itens { get; set; }
        public ICollection<Parcela> Parcelas { get; set; }
    }
}
