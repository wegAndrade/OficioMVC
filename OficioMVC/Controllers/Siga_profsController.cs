using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OficioMVC.Models;
using OficioMVC.Models.ViewModels;
using OficioMVC.Service;

namespace OficioMVC.Controllers
{
    public class Siga_profsController : Controller
    {
        private readonly OficioMVCContext _context;
        private readonly Siga_profsService _profsService;
        private readonly HashPass _hash;

        public Siga_profsController(OficioMVCContext context, Siga_profsService profsService, HashPass hash)
        {
            _context = context;
            _profsService = profsService;
            _hash = hash;
        }



        public async Task<IActionResult> Verify(String user_login)
        {
            if (user_login == null)
            {
                return NotFound();
            }
            

            var siga_profs = await _context.Siga_profs
                .FirstOrDefaultAsync(m => m.user_login == user_login);
            if (siga_profs == null)
            {
                return NotFound();
            }

            return View(siga_profs);
        }

        public IActionResult Authorization(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public IActionResult Authorization(int Id, Siga_profs Usuario)
        {
            string HashPass = _hash.GerarMD5(Usuario.user_pass);
            var obj = _profsService.FindByUser(Usuario.user_login, HashPass);

            if (obj.master != false)
            {
                return RedirectToAction("Edit","Documentos", new { Id = Id, authorization = true });
            }

            return RedirectToAction("Edit","Error", new { message = "Acesso negado" });
        }

    }
}
