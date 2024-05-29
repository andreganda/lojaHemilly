using lojaHemilly.DataBase;
using lojaHemilly.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace lojaHemilly.Service
{
    public interface IClienteService
    {
        Task<Cliente> Create(Cliente cliente);
    }

    public class ClienteService : IClienteService
    {
        private readonly FlorDeLizContext _context;

        public ClienteService(FlorDeLizContext context)
        {
            _context = context;
        }

        public async Task<Cliente> Create(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }
    }
}
