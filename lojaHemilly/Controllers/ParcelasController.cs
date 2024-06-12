using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lojaHemilly.DataBase;
using lojaHemilly.Models;
using lojaHemilly.Service;

namespace lojaHemilly.Controllers
{
    public class ParcelasController : Controller
    {
        private readonly FlorDeLizContext _context;
        private readonly IVendaService _vendaService;

        public ParcelasController(FlorDeLizContext context, IVendaService vendaService)
        {
            _context = context;
            _vendaService = vendaService;
        }

        // GET: Parcelas
        public async Task<IActionResult> Index(int id)
        {
            var venda = await _vendaService.Detail(id);
            ViewData["cliente"] = venda.Cliente;
            ViewData["venda"] = venda;

            var florDeLizContext = _context.Parcelas.Where(a=> a.VendaId == id).Include(p => p.Venda);


            return View(await florDeLizContext.ToListAsync());
        }


        #region CRIAÇÃO AUTOMATICA

        // GET: Parcelas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcela = await _context.Parcelas
                .Include(p => p.Venda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcela == null)
            {
                return NotFound();
            }

            return View(parcela);
        }

        // GET: Parcelas/Create
        public IActionResult Create()
        {
            ViewData["VendaId"] = new SelectList(_context.Vendas, "Id", "Id");
            return View();
        }

        // POST: Parcelas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VendaId,NumeroParcela,Valor,DataVencimento,Pago")] Parcela parcela)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parcela);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendaId"] = new SelectList(_context.Vendas, "Id", "Id", parcela.VendaId);
            return View(parcela);
        }

        // GET: Parcelas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcela = await _context.Parcelas.FindAsync(id);
            if (parcela == null)
            {
                return NotFound();
            }
            ViewData["VendaId"] = new SelectList(_context.Vendas, "Id", "Id", parcela.VendaId);
            return View(parcela);
        }

        // POST: Parcelas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VendaId,NumeroParcela,Valor,DataVencimento,Pago")] Parcela parcela)
        {
            if (id != parcela.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parcela);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParcelaExists(parcela.Id))
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
            ViewData["VendaId"] = new SelectList(_context.Vendas, "Id", "Id", parcela.VendaId);
            return View(parcela);
        }

        // GET: Parcelas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parcela = await _context.Parcelas
                .Include(p => p.Venda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parcela == null)
            {
                return NotFound();
            }

            return View(parcela);
        }

        // POST: Parcelas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parcela = await _context.Parcelas.FindAsync(id);
            if (parcela != null)
            {
                _context.Parcelas.Remove(parcela);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParcelaExists(int id)
        {
            return _context.Parcelas.Any(e => e.Id == id);
        } 
        #endregion
    }
}
