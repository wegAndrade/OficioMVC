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
using OficioMVC.Libraries.Login;

namespace OficioMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly Siga_profsService _profsService;
        private readonly OficioMVCContext _context;
        private readonly HashPass _hash;
        private readonly LoginUser _login;

        public HomeController(Siga_profsService profsService, OficioMVCContext context, HashPass hash, LoginUser login)
        {
            _profsService = profsService;
            _hash = hash;
            _context = context;
            _login = login;

        }
        [HttpGet]
        public IActionResult Index()
        {

            return View();



           
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

                string HashPass = _hash.GerarMD5(s.user_pass);
                //Verificando as 
                var obj = _profsService.FindByUser(s.user_login, HashPass);

                //var obj = _context.Siga_profs.Where(x => x.user_login == s.user_login && x.user_pass == s.user_pass).FirstOrDefault();
                if (obj != null)
                {
                    _login.Login(obj);
                    //HttpContext.Session.SetObjectAsJson("User", obj);
                    return RedirectToAction("Index", "Documentos");
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
            _login.Logout();
            //HttpContext.Session.SetObjectAsJson("User", null);
            return RedirectToAction("Login");
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();




        }

        public IActionResult Contact()
        {


            ViewData["Message"] = "Your contact page.";

            return View();




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
        public IActionResult AcessoNegado()
        {
            return View();
        }

        
    }
}
