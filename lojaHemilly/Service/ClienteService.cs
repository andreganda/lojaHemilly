using lojaHemilly.DataBase;
using lojaHemilly.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace lojaHemilly.Service
{
    public interface IClienteService
    {
        Task<Cliente> Create(Cliente cliente);
        Task<Cliente?> Update(Cliente cliente);
        Task<Cliente> VerificarEmailExistenteAsync(string email);
        Task<Cliente?> Detail(int id);
        Task<bool> DeleteForever(int id);
        Task<bool> DeleteOff(int id);
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

            cliente.DataCadastro = DateTime.Now;
            cliente.DataAlteracaoCadastro = null;
            cliente.Nome = cliente.Nome.ToUpper();
            cliente.Endereco = cliente.Endereco?.ToUpper();

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente?> Update(Cliente cliente)
        {
            try
            {
                var clienteDb = await _context.Clientes.FindAsync(cliente.ClienteID);

                if (clienteDb != null)
                {
                    clienteDb.DataAlteracaoCadastro = DateTime.Now;
                    _context.Update(clienteDb);
                    await _context.SaveChangesAsync();
                    return clienteDb;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Cliente?> Detail(int id)
        {
            try
            {
                return await _context.Clientes.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteForever(int id)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if (cliente != null)
                {
                    _context.Clientes.Remove(cliente);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
               return false;    
            }
        }

        public async Task<bool> DeleteOff(int id)
        {
            try
            {
                var clienteDb = await _context.Clientes.FindAsync(id);

                if (clienteDb != null)
                {
                    clienteDb.Status = 0;
                    _context.Update(clienteDb);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }



        }
        public async Task<Cliente> VerificarEmailExistenteAsync(string email)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
