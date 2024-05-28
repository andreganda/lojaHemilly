﻿namespace lojaHemilly.Models
{
    public class ItemPedido
    {
        public int ItemPedidoID { get; set; }
        public int PedidoID { get; set; }
        public int ProdutoID { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }
    }
}
