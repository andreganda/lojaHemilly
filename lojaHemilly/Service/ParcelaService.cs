using lojaHemilly.DataBase;
using lojaHemilly.Models;

namespace lojaHemilly.Service
{
    public interface IParcelaService
    {
        Task<Parcela> Create(Parcela parcela);
        Task<Parcela> Update(Parcela parcela);
    }

    public class ParcelaService : IParcelaService
    {
        private readonly FlorDeLizContext _context;
        public ParcelaService(FlorDeLizContext context)
        {
            _context = context;
        }

        public async Task<Parcela> Create(Parcela parcela)
        {
            _context.Parcelas.Add(parcela);
            await _context.SaveChangesAsync();
            return parcela;
        }

        public async Task<Parcela> Update(Parcela parcela)
        {
            try
            {
                _context.Update(parcela);
                await _context.SaveChangesAsync();
                return parcela;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
