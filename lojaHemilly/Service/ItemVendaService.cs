using lojaHemilly.DataBase;
using lojaHemilly.Models;

namespace lojaHemilly.Service
{
    public interface IItemVendaService
    {
        Task<ItemVenda> Create(ItemVenda item);
    }

    public class ItemVendaService : IItemVendaService
    {
        private readonly FlorDeLizContext _context;

        public ItemVendaService(FlorDeLizContext context)
        {
            _context = context;
        }

        public async Task<ItemVenda> Create(ItemVenda item)
        {
            _context.ItemVendas.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
