using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OficioMVC.Models;
using OficioMVC.Models.ViewModels;

namespace OficioMVC.Controllers
{
    public class DocumentosController : Controller
    {
        private readonly OficioMVCContext _context;

        public DocumentosController(OficioMVCContext context)
        {
            _context = context;
        }

        // GET: Documentoes
        public async Task<IActionResult> Index()
        {
            var oficioMVCContext = _context.Documento.Include(d => d.Usuario);
            return View(await oficioMVCContext.ToListAsync());
        }

        // GET: Documentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documento = await _context.Documento
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documento == null)
            {
                return NotFound();
            }

            return View(documento);
        }

        // GET: Documentoes/Create
        public IActionResult Create()
        {
            var usuario =  _context.Siga_profs.FirstOrDefault();
            var viewModel = new DocumentoViewModel { Usuario =  usuario };
            ViewData["UsuarioId"] = new SelectList(_context.Siga_profs, "ID", "user_login");
            return View(viewModel);
            
            
        }

        // POST: Documentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Documento documento)
        {
            var obj = documento;
            if (ModelState.IsValid)
            {
                _context.Add(documento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var usuario = _context.Siga_profs.FirstOrDefault();
            var viewModel = new DocumentoViewModel { Usuario = usuario };
            ViewData["UsuarioId"] = new SelectList(_context.Siga_profs, "ID", "user_login");
            return View(viewModel);
        }

        // GET: Documentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documento = await _context.Documento.FindAsync(id);
            if (documento == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Siga_profs, "ID", "user_login", documento.UsuarioId);
            return View(documento);
        }

        // POST: Documentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numeracao,Ano,Assunto,Observacoes,Tipo,CaminhoArq,DataEnvio,DataAlteracao,UsuarioId")] Documento documento)
        {
            if (id != documento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentoExists(documento.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Siga_profs, "ID", "user_login", documento.UsuarioId);
            return View(documento);
        }

        // GET: Documentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documento = await _context.Documento
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (documento == null)
            {
                return NotFound();
            }

            return View(documento);
        }

        // POST: Documentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documento = await _context.Documento.FindAsync(id);
            _context.Documento.Remove(documento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentoExists(int id)
        {
            return _context.Documento.Any(e => e.Id == id);
        }
    }
}
