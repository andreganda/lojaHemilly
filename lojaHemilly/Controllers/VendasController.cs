using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lojaHemilly.DataBase;
using lojaHemilly.Models;

namespace lojaHemilly.Controllers
{
    public class VendasController : Controller
    {
        private readonly FlorDeLizContext _context;

        public VendasController(FlorDeLizContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var florDeLizContext = _context.Vendas.Include(v => v.Cliente);
            return View(await florDeLizContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        public IActionResult Create()
        {

            //select Item
            ViewData["ClienteId"] = ListaClientesSelectItem();
            return View();
        }

        private IEnumerable<SelectListItem> ListaClientesSelectItem()
        {
            var listaManual = new List<SelectListItem>
            {
                new SelectListItem { Text = "SELECIONE UM CLIENTE ", Value = "" }
            };

            // Criando a lista selecionável a partir dos clientes
            var listaSelecionavelClientes = new SelectList(_context.Clientes.Where(a=> a.Status == 1), "ClienteID", "Nome");

            // Concatenando a lista manual com a lista selecionável dos clientes
            var listaCompleta = listaManual.Concat(listaSelecionavelClientes);

            return listaCompleta;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataDaVenda,ClienteId,PrecoTotal,NumeroParcelas,Total")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteID", "Nome", venda.ClienteId);
            return View(venda);
        }

        /// <summary>
        /// SALVAR A VENDA CRIADA
        /// </summary>
        /// <param name="venda"></param>
        [HttpPost]
        public int SalvarVenda(VendaViewModel venda)
        {
            return 1;
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteID", "Nome", venda.ClienteId);
            return View(venda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataDaVenda,ClienteId,PrecoTotal,NumeroParcelas,Total")] Venda venda)
        {
            if (id != venda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaExists(venda.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteID", "Nome", venda.ClienteId);
            return View(venda);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Vendas
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda != null)
            {
                _context.Vendas.Remove(venda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(int id)
        {
            return _context.Vendas.Any(e => e.Id == id);
        }




        public class VendaViewModel
        {
            public string DataDaVenda { get; set; }
            public int ClienteId { get; set; }
            public decimal PrecoTotal { get; set; }
            public int NumeroParcelas { get; set; }
            public string Total { get; set; }
            public string Entrada { get; set; }
        }
    }
}
