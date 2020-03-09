using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OficioMVC.Model;
using OficioMVC.Models;
using OficioMVC.Service;

namespace OficioMVC.Controllers
{
    public class Siga_profsControllerold : Controller
    {
        private readonly Siga_profsService _siga_profsService;

        public Siga_profsControllerold(Siga_profsService siga_profsService)
        {
            _siga_profsService = siga_profsService;
        }


        // GET: Siga_profs
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Verify( Siga_profs acc)
        {
            if (acc.user_login == null)
            {
                return View(RedirectToAction(nameof(Error), new { message = "Usuario vazio" }));

            }
            if (acc.user_pass == null)
            {
                return View(RedirectToAction(nameof(Error), new { message = "Senha vazia" }));

            }
            var obj = _siga_profsService.FindByUserAndPassAsync(acc.user_login, acc.user_pass);

            
            
            if (obj == null)
            {
                return View(RedirectToAction(nameof(Error), new { message = "Usuario Inexistente" }));
            }
            return View(obj);
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
