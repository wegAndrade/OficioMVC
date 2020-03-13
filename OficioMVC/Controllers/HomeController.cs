using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OficioMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;

namespace OficioMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly OficioMVCContext _context;

        public HomeController(OficioMVCContext context)
        {
            _context = context;
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
            return View();
        }
        [HttpPost, ActionName("Login")]
        public IActionResult Login(Siga_profs s)
        {
            if (ModelState.IsValid) //verifica se é válido
            {


                var obj = _context.Siga_profs.Where(x => x.user_login == s.user_login && x.user_pass == s.user_pass).FirstOrDefault();

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
