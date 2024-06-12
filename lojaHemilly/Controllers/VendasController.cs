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
using MySqlConnector;
using System.Data.SqlClient;

namespace lojaHemilly.Controllers
{
    public class VendasController : Controller
    {
        private readonly FlorDeLizContext _context;
        private readonly IVendaService _vendaService;
        private readonly IItemVendaService _itemVendaService;
        private readonly IParcelaService _parcelaService;

        public VendasController(
            FlorDeLizContext context,
            IVendaService vendaService,
            IItemVendaService itemVendaService,
            IParcelaService parcelaService)
        {
            _context = context;
            _vendaService = vendaService;
            _itemVendaService = itemVendaService;
            _parcelaService = parcelaService;
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
            var listaSelecionavelClientes = new SelectList(_context.Clientes.Where(a => a.Status == 1), "ClienteID", "Nome");

            // Concatenando a lista manual com a lista selecionável dos clientes
            var listaCompleta = listaManual.Concat(listaSelecionavelClientes);

            return listaCompleta;
        }


        [HttpPost]
        public async Task<Mensagem> SalvarVendaAsync(VendaViewModel venda)
        {
            var m = new Mensagem();
            try
            {
                decimal entrada = decimal.Parse(venda.Entrada.Replace("R$", "").Trim());
                decimal total = Convert.ToDecimal(venda.Total.Replace("R$", "").Trim());

                var novaVenda = new Venda();
                novaVenda.Entrada = entrada;
                novaVenda.Total = total;
                novaVenda.DataDaVenda = Convert.ToDateTime(venda.DataDaVenda);
                novaVenda.ClienteId = venda.ClienteId;
                novaVenda.TipoPagamento = venda.TipoFormaPagamento;

                //venda a vista;
                if (venda.TipoFormaPagamento == 2)
                {
                    novaVenda.NumeroParcelas = 0;
                    novaVenda.Status = 1;
                }
                else
                {
                    novaVenda.NumeroParcelas = venda.NumeroParcelas;
                    novaVenda.Status = 0;
                }

                var vendaIncluida = await _vendaService.Create(novaVenda);

                if (vendaIncluida.Id != 0)
                {
                    await SalvarItensVendaAsync(vendaIncluida.Id, venda.ItensVenda);

                    //Criando parcelas
                    if (venda.TipoFormaPagamento == 1)
                        await SalvarParcelasAsync(vendaIncluida);
                }
                m.Status = 1;
                m.Descricao = "Compra incluída com sucesso";
                return m;
            }
            catch (Exception ex)
            {
                m.Status = 0;
                m.Descricao = ex.Message.ToString();
                return m;
            }
        }

        private async Task<Mensagem> SalvarItensVendaAsync(int idVenda, IEnumerable<ItemVendaDto> itens)
        {
            Mensagem m = new Mensagem();
            try
            {
                foreach (var item in itens)
                {
                    var novoItem = new ItemVenda();
                    novoItem.NomeDoProduto = item.NomeDoProduto.ToUpper();
                    novoItem.VendaId = idVenda;
                    novoItem.PrecoUnitario = Convert.ToDecimal(item.PrecoUnitario.Replace(".", ","));
                    novoItem.Quantidade = item.Quantidade;
                    await _itemVendaService.Create(novoItem);
                }

                m.Status = 1;
            }
            catch (Exception ex)
            {
                m.Status = 0;
                m.Descricao = ex.Message;
                return m;
            }

            return m;
        }

        private async Task<bool> SalvarParcelasAsync(Venda venda)
        {
            var valorParcela = (venda.Total - venda.Entrada) / venda.NumeroParcelas;

            try
            {
                for (var i = 1; i <= venda.NumeroParcelas; i++)
                {
                    var dataVenda = venda.DataDaVenda;
                    var parcela = new Parcela();
                    parcela.VendaId = venda.Id;
                    parcela.NumeroParcela = i;
                    parcela.DataVencimento = dataVenda.AddMonths(i);
                    parcela.Valor = valorParcela;
                    parcela.Pago = false;
                    await _parcelaService.Create(parcela);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        #region METODOS DEFAULT
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



        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, int? status)
        {

            if (!status.HasValue)
                status = 0;

            var query = $"";

            if (status == -1)
            {
                query = $"(status = 0 or status = 1) ";
            }
            else
            {
                query = $"(status = {status}) ";
            }

            if (startDate != null)
            {
                string de = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(startDate));
                query += $" and (date(DataDaVenda) >= '{de}') ";
            }

            if (endDate != null)
            {
                string ate = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(endDate));
                query += $"and (date(DataDaVenda) <= '{ate}') ";
            }

            var sqlQuery = $"SELECT * FROM vendas where {query}";

            var vendas = await _context.Vendas.FromSqlRaw(sqlQuery).Include(v => v.Cliente).Include(a => a.Parcelas).ToListAsync();

            ViewData["StartDate"] = startDate != null ? startDate.Value.ToString("yyyy-MM-dd") : "";
            ViewData["EndDate"] = endDate != null ? endDate.Value.ToString("yyyy-MM-dd") : "";
            ViewData["status"] = status.ToString();

            ViewData["ListaSeletFiltro"] = ListaSelect();

            return View(vendas);
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

        #endregion

        public class VendaViewModel
        {
            public string DataDaVenda { get; set; }
            public int ClienteId { get; set; }
            public int NumeroParcelas { get; set; }
            public string Total { get; set; }
            public string Entrada { get; set; }
            public int TipoFormaPagamento { get; set; }
            public IEnumerable<ItemVendaDto> ItensVenda { get; set; }
        }

        public class ItensSelectStatus
        {
            public int Value { get; set; }
            public string? Descricao { get; set; }
        }

        public List<ItensSelectStatus> ListaSelect()
        {
            var lista = new List<ItensSelectStatus>();
            lista.Add(new ItensSelectStatus { Value = 0, Descricao = "Pendente" });
            lista.Add(new ItensSelectStatus { Value = -1, Descricao = "Todos" });
            lista.Add(new ItensSelectStatus { Value = 1, Descricao = "Pago" });
            return lista;
        }
    }
}
