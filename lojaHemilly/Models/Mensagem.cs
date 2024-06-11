namespace lojaHemilly.Models
{
    public class Mensagem
    {
        public int Id { get; set; } 
        public string ?Descricao { get; set;}

        /// <summary>
        /// 1 -> TUDO OK
        /// 2 -> ERRO
        /// </summary>
        public int Status { get; set; }
    }
}
