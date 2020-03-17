using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OficioMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using OficioMVC.Service;

namespace OficioMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly Siga_profsService _profsService;
        private readonly OficioMVCContext _context;
        private readonly HashPass _hash;

        public HomeController(Siga_profsService profsService, OficioMVCContext context, HashPass hash)
        {
            _profsService = profsService;
            _context = context;
            _hash = hash;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (UsarioLogado())
            {
                return View();
            }


            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            if (UsarioLogado())
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost, ActionName("Login")]
        public IActionResult Login(Siga_profs s)
        {
            if (ModelState.IsValid) //verifica se é válido
            {

               string HashPass =  _hash.GerarMD5(s.user_pass);
                //Verificando as 
                var obj = _profsService.FindByUser(s.user_login, HashPass);

                //var obj = _context.Siga_profs.Where(x => x.user_login == s.user_login && x.user_pass == s.user_pass).FirstOrDefault();
                if (obj != null)
                {
                    HttpContext.Session.SetObjectAsJson("User", obj);
                    return RedirectToAction("Index");
                    
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }

            return View(s);
        }

        public IActionResult Logout(Siga_profs s)
        {
            HttpContext.Session.SetObjectAsJson("User", null);
            return RedirectToAction("Login");
        }
        public IActionResult About()
        {
            if (UsarioLogado())
            {
                ViewData["Message"] = "Your application description page.";

                return View();
            }


            return RedirectToAction("Login");
        }

        public IActionResult Contact()
        {
            if (UsarioLogado())
            {
                ViewData["Message"] = "Your contact page.";

                return View();
            }


            return RedirectToAction("Login");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public bool UsarioLogado()
        {
            var myUser = HttpContext.Session.GetObjectFromJson<Siga_profs>("User");
            if (myUser == null)
            {
                return false;
            }
            if (myUser.user_login != null && myUser.ID != null)
            {
                string ativo = myUser.ativo.ToUpper();
                if (ativo == "ATIVO")
                {
                    return true;
                }

            }
            return false;

        }
    }
}
