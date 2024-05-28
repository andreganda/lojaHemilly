namespace lojaHemilly.Models
{
    public class Produto
    {
        public int ProdutoID { get; set; }
        public string NomeProduto { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int CategoriaID { get; set; }

        public Categoria Categoria { get; set; }
    }
}
