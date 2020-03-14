using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OficioMVC.Models;

namespace OficioMVC.Controllers
{
    public class Siga_profsController : Controller
    {
        private readonly OficioMVCContext _context;

        public Siga_profsController(OficioMVCContext context)
        {
            _context = context;
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
    
  

    }
}
