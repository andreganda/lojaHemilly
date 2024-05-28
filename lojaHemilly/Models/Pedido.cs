using System.ComponentModel.DataAnnotations;

namespace lojaHemilly.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoID { get; set; }
        public int ClienteID { get; set; }
        public DateTime DataPedido { get; set; }
        public string Status { get; set; }
        public decimal ValorTotal { get; set; }

        public Cliente Cliente { get; set; }
        public List<ItemPedido> ItensPedido { get; set; } = new List<ItemPedido>();
        public List<ParcelaPedido> ParcelasPedido { get; set; } = new List<ParcelaPedido>();
    }
}
