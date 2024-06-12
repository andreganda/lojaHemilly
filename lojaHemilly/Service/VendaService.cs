using lojaHemilly.DataBase;
using lojaHemilly.Models;
using Microsoft.EntityFrameworkCore;

namespace lojaHemilly.Service
{
    public interface IVendaService
    {
        Task<Venda> Create(Venda venda);
        Task<Venda> Detail(int id);
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


        public async Task<Venda> Detail(int id)
        {
            try
            {
                var venda = await _context.Vendas.Include(a=> a.Cliente).FirstOrDefaultAsync(a=> a.Id == id);
                return venda;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
