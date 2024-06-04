namespace lojaHemilly.Models
{
    public class Parcela
    {
        public int Id { get; set; }
        public int VendaId { get; set; }
        public int NumeroParcela { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Pago { get; set; }
        public Venda Venda { get; set; }
    }
}
