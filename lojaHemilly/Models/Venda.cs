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
        

        /// <summary>
        /// Caso o cliente dê algum valor de entrada na compra para abater na criaçao 
        /// das parcelas.
        /// </summary>
        public decimal Entrada { get; set; }


        /// <summary>
        /// 1 - PARCELADO
        /// 2 - A VISTA
        /// </summary>
        public int TipoPagamento { get; set; }

        /// <summary>
        /// Verifica se a venda esta paga ou não
        /// 1-> Compra ainda não paga
        /// 2-> Compra toda paga, todas as parcelas foram pagas
        /// </summary>
        public int Status { get; set; }
        public ICollection<ItemVenda> Itens { get; set; }
        public ICollection<Parcela> Parcelas { get; set; }
    }
}
