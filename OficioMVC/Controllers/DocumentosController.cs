using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OficioMVC.Libraries.Arquivo;
using OficioMVC.Libraries.Filtro;
using OficioMVC.Libraries.Login;
using OficioMVC.Models;
using OficioMVC.Models.Enums;
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
        UploadFile _arquivo;



        public DocumentosController(OficioMVCContext context, LoginUser login, DocumentoService documentoService, UploadFile arquivo)
        {
            _context = context;
            _login = login;
            _documentoService = documentoService;
            _arquivo = arquivo;
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
        public IActionResult Create(int? T)
        {

            int typeId = (int)T;
            TipoDoc T1 = (TipoDoc)T;
            string typeName = T1.ToString();
            ViewBag.typeId = typeId;
            ViewBag.typeName = typeName;
            Siga_profs Usuario = _login.GetUser();
            ViewBag.Siga_profs = Usuario;
            return View();
        }
        public IActionResult CreateEdital()
        {
            int T1 = (int)TipoDoc.Edital;
            return RedirectToAction("Create", "Documentos", new { T = T1, area = "" });
        }
        public IActionResult CreateOficio()
        {
            int T1 = (int)TipoDoc.Oficio;
            return RedirectToAction("Create", "Documentos", new { T = T1, area = "" });
        }
        public IActionResult CreateMemorando()
        {
            int T1 = (int)TipoDoc.Memorando;
            return RedirectToAction("Create", "Documentos", new { T = T1, area = "" });
        }
        public IActionResult CreatePortaria()
        {
            int T1 = (int)TipoDoc.Portaria;
            return RedirectToAction("Create", "Documentos", new { T = T1, area = "" });
        }
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
            ViewBag.Doc_identificador = Convert.ToString(documento.Numeracao) + "/" + Convert.ToString(documento.Ano);
            return View(documento);
        }

        // POST: Documentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numeracao,Ano,Assunto,Observacoes,Tipo,CaminhoArq,DataEnvio,DataAlteracao,UsuarioId")] Documento documento, IFormFile file)
        {
            if (id != documento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    string fileNewName = Convert.ToString(documento.Numeracao) + "_" + Convert.ToString(documento.Ano);
                    string fileNameExt;
                    try
                    {
                        fileNameExt = _arquivo.Upload(file, fileNewName);
                    }
                    catch (IOException e)
                    {
                        return RedirectToAction(nameof(Error), new { message = "Documento já existe"  + e.Message});
                    }
                    if (_arquivo.FileExist(fileNameExt))
                    {
                        documento.CaminhoArq = fileNameExt;
                    }
                    documento.DataAlteracao = DateTime.Now;
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

        public ActionResult DownloadFile(string arquivo)
        {
            if (arquivo == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", "Arquivos", arquivo);

            FileStream fileStream;

            try
            {
                fileStream = System.IO.File.OpenRead(path);
            }
            catch (DirectoryNotFoundException)
            {
                return new EmptyResult();
            }


            return File(fileStream, path, arquivo);
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }



    }
}
