namespace lojaHemilly.Models
{
    public class ItemCompra
    {
        public int ItemCompraID { get; set; }
        public int CompraID { get; set; }
        public int ProdutoID { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public Compra Compra { get; set; }
        public Produto Produto { get; set; }
    }
}
