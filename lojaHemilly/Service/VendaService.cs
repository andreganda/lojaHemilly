using lojaHemilly.DataBase;
using lojaHemilly.Models;

namespace lojaHemilly.Service
{
    public interface IVendaService
    {
        Task<Venda> Create(Venda venda);
    }

    public class VendaService : IVendaService
    {
        private readonly FlorDeLizContext _context;
        public VendaService(FlorDeLizContext context)
        {
            _context = context;
        }

        public async Task<Venda> Create(Venda venda)
        {              
            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();
            return venda;
        }
    }
}
