using lojaHemilly.Models;
using Microsoft.EntityFrameworkCore;

namespace lojaHemilly.DataBase
{
    public class FlorDeLizContext : DbContext
    {
        public FlorDeLizContext(DbContextOptions<FlorDeLizContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItemVendas { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }
    }
}
