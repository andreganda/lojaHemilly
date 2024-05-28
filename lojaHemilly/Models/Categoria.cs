namespace lojaHemilly.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string NomeCategoria { get; set; }

        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
