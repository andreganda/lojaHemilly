using lojaHemilly.Models;
using Microsoft.EntityFrameworkCore;

namespace lojaHemilly.DataBase
{
    public class FlorDeLizContext : DbContext
    {
        public FlorDeLizContext(DbContextOptions<FlorDeLizContext> options) : base(options) { }

        public DbSet<Cliente> Cliente { get; set; }
    }
}
