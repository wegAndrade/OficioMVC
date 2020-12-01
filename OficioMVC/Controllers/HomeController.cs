using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OficioMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OficioMVC.Service;
using OficioMVC.Libraries.Login;
using OficioMVC.Libraries.Autenticacao;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace OficioMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly Siga_profsService _profsService;
        private readonly OficioMVCContext _context;
        private readonly HashPass _hash;
        private readonly IAutenticacao _autentica;

        public HomeController(Siga_profsService profsService, OficioMVCContext context, HashPass hash, IAutenticacao autentica)
        {
            _profsService = profsService;
            _hash = hash;
            _context = context;
            _autentica = autentica;

        }
        [AllowAnonymous]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost, ActionName("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Siga_profs s)
        {
            if (ModelState.IsValid) //verifica se é válido
            {

                bool valida = _autentica.ValidarLogin(s);
                

                //var obj = _context.Siga_profs.Where(x => x.user_login == s.user_login && x.user_pass == s.user_pass).FirstOrDefault();
                if (valida)
                {
                    var user = _profsService.FindByUser(s.user_login, _hash.GerarMD5(s.user_pass));

                    var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Name, user.user_login),
                       };
                    claims.Add(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(user)));


                    var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    HttpContext.User = principal;

                    await HttpContext.SignInAsync(principal);


                    return RedirectToAction("Index", "Documentos");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }

            return View(s);
        }

        public async Task<IActionResult> Logout(Siga_profs s)
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
       
      

        [AllowAnonymous]
        public IActionResult AcessoNegado()
        {
            return View();
        }

        
    }
}
