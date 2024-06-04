namespace lojaHemilly.Models
{
    public class VendaDto
    {
        public int ClienteId { get; set; }
        public int NumeroParcelas { get; set; }
        public List<ItemVendaDto>? Itens { get; set; }
    }

    public class ItemVendaDto
    {
        public string? NomeDoProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }

    public class ParcelaDto
    {
        public int NumeroParcela { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Pago { get; set; }
    }
}
