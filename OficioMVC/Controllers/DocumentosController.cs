using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OficioMVC.Libraries.Filtro;
using OficioMVC.Libraries.Login;
using OficioMVC.Models;
using OficioMVC.Models.ViewModels;
using OficioMVC.Service;

namespace OficioMVC.Controllers
{
    [UsuarioAutorizacao]
    public class DocumentosController : Controller
    {
        private readonly OficioMVCContext _context;
        private readonly LoginUser _login;
        private readonly DocumentoService _documentoService;

        
        
        public DocumentosController(OficioMVCContext context, LoginUser login, DocumentoService documentoService)
        {
            _context = context;
            _login = login;
            _documentoService = documentoService;
        }

       
        // GET: Documentoes
        public async Task<IActionResult> Index()
        {
                 var oficioMVCContext = _context.Documento.Include(d => d.Usuario);
                return View(await oficioMVCContext.ToListAsync());
        }

        public  IActionResult CreateEdital()
        {
            Siga_profs Usuario = _login.GetUser();
            ViewBag.Siga_profs = Usuario.ID;
            ViewBag.Siga_profs = Usuario.user_login;
            return View("Create");
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
        public async Task<IActionResult> Create()
        {
            

            Siga_profs Usuario = _login.GetUser();
            ViewBag.Siga_profs = Usuario;



          
            return View();
            
            
        }

        // POST: Documentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Documento documento)
        {
            documento.UsuarioId = _login.GetUser().ID;
            documento.Numeracao = _documentoService.GetNumMax();
            documento.Ano = DateTime.Now.Year;
            documento.DataEnvio = DateTime.Now;

            if (ModelState.IsValid)
            {
               
                await _documentoService.InsertAsync(documento);
                
            }
            return RedirectToAction("Index");
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
