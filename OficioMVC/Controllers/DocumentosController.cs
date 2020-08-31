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
using OficioMVC.Libraries.Filtro;
using OficioMVC.Libraries.Login;
using OficioMVC.Models;
using OficioMVC.Models.Enums;
using OficioMVC.Models.ViewModels;
using OficioMVC.Service;
using System.Web.Mvc;

namespace OficioMVC.Controllers
{
    [UsuarioAutorizacao]
    public class DocumentosController : Controller
    {
        private readonly OficioMVCContext _context;
        private readonly LoginUser _login;
        private readonly DocumentoService _documentoService;
        private readonly FileService _arquivo;
        private readonly Siga_profsService _user;
        private readonly HashPass _hash;

        public DocumentosController(OficioMVCContext context, LoginUser login, DocumentoService documentoService, FileService arquivo, Siga_profsService user, HashPass hash)
        {
            _context = context;
            _login = login;
            _documentoService = documentoService;
            _arquivo = arquivo;
            _user = user;
            _hash = hash;
        }


        // GET: Documentoes

        public async Task<IActionResult> Index()
        {
            var oficioMVCContext = _context.Documento.Include(d => d.Usuario).Where(d => d.Status != StatusDoc.Excluido);
            return View(await oficioMVCContext.ToListAsync());
        }
        //Detalhando o documento selecionado
        // GET: Documentoes/Details/5
        public async Task<IActionResult> Details(int? id, Boolean alterado)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documento = await _documentoService.FindById(id);
            /* var documento = await _context.Documento
                 .Include(d => d.Usuario)
                 .FirstOrDefaultAsync(m => m.Id == id);*/
            if (documento == null)
            {
                return NotFound();
            }
            if (alterado)
            {
                @ViewBag.alterado = alterado;
            }
            return View(documento);
        }
        // GET: Documentoes/Create
        //Retornando o formulario para criação de documento, recebe um INT referente ao ENUM do tipo de Documento
        public IActionResult Create(int T)
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
        // Actions para Criação de cada tipo de Documento
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
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([FromBody] Documento documento)
        {

            documento.UsuarioId = _login.GetUser().ID;
            documento.Numeracao = _documentoService.GetNumMax();
            documento.Ano = DateTime.Now.Year;
            documento.DataEnvio = DateTime.Now;
            documento.Status = StatusDoc.Aberto;

            if (ModelState.IsValid)
            {
                try
                {
                    await _documentoService.InsertAsync(documento);

                    documento.Usuario = _login.GetUser();
                    return Json(documento);
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    return Json(e.Message);
                }


            }
            else
            {
                return Json("error:errou!");
            }
        }
        //Edição envio da view de formulario
        // GET: Documentoes/Edit/5
        public async Task<IActionResult> Edit(int? id, bool? authorization)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documento = await _documentoService.FindById(id);
            if (documento == null)
            {
                return NotFound();
            }
            if (authorization == false || authorization == null)
            {
                if (documento.Status == StatusDoc.Enviado || documento.Status == StatusDoc.Excluido)
                {
                    return RedirectToAction("Authorization", "Siga_profs", new { id = documento.Id });
                    

                }
            }
            //Passando para View a formatação da númeração de documentos
            ViewBag.Doc_identificador = Convert.ToString(documento.Numeracao) + "/" + Convert.ToString(documento.Ano);

            var ViewModel = new DocumentoViewModel { Documento = documento, Usuario = _login.GetUser(), Tipos = _documentoService.GetAllTypes() };
            ViewModel.teste();
            return View(ViewModel);
        }
                
        // GET: Documentoes/Delete/5
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
                    //verifica se foi enviado arquivo.
                    if (file != null)
                    {
                        string fileNewName = Convert.ToString(documento.Numeracao) + "_" + Convert.ToString(documento.Ano);
                        string fileNameExt;
                        try
                        {
                            fileNameExt = _arquivo.Upload(file, fileNewName);
                        }
                        catch (IOException e)
                        {
                            return RedirectToAction(nameof(Error), new { message = "Documento já existe" + e.Message });
                        }
                        if (_arquivo.FileExist(fileNameExt))
                        {
                            documento.CaminhoArq = fileNameExt;
                        }
                        documento.Status = StatusDoc.Enviado;
                    }
                    //caso não enviado mantem o caminho de arquivo anterior.
                    if (file == null)
                    {
                        documento.CaminhoArq =
                           _documentoService.GetCaminhoArq(id);
                        documento.Status = StatusDoc.Aberto;
                    }



                    documento.DataAlteracao = DateTime.Now;
                    await _documentoService.UpdateAsync(documento);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_documentoService.DocumentoExists(documento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = documento.Id,alterado = true });
            }
            ViewData["UsuarioId"] = new SelectList(_context.Siga_profs, "ID", "user_login", documento.UsuarioId);
            return View(documento);
        }
      
        // POST: Documentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documento = await _context.Documento.FindAsync(id);
            documento.Status = 0;
            _context.Documento.Update(documento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
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


        public ActionResult Download(string CaminhoArq)
        {
            if (CaminhoArq == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Arquivo inexistente faça o Upload do mesmo" });
            }
            try
            {
                return _arquivo.Download(CaminhoArq);
            }
            catch (IOException e)
            {
                return RedirectToAction(nameof(Error), new { Message = "Arquivo inexistente faça o Upload do mesmo. " + e.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> SubstituirArquivo(int? id)
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
        [HttpPost]
        public IActionResult SubstituirArquivo(IFormFile file, Documento documento)
        {
            string name = Convert.ToString(documento.Numeracao) + "_" + Convert.ToString(documento.Ano);
            _arquivo.ReplaceFile(file, name, documento.CaminhoArq);
            return RedirectToAction(nameof(Index));
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
