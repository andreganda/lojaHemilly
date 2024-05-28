namespace lojaHemilly.Models
{
    public class Cliente
    {
        public int ClienteID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public DateTime DataCadastro { get; set; }

        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
