using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace lojaHemilly.Models
{
    public class ParcelaPedido
    {
        [Key]
        public int ParcelaID { get; set; }
        public int PedidoID { get; set; }
        public int NumeroParcela { get; set; }
        public decimal ValorParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public string StatusPagamento { get; set; }

        public Pedido Pedido { get; set; }
    }
}
