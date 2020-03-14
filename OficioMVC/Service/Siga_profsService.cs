using Microsoft.EntityFrameworkCore;
using OficioMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Service
{
    public class Siga_profsService
    {
        private readonly OficioMVCContext _context;

        public Siga_profsService(OficioMVCContext context)
        {
            _context = context;
        }


        public async Task<Siga_profs> FindByIdAsync(int id)
        {
            return await _context.Siga_profs.Include(obj => obj.dpto).FirstOrDefaultAsync(obj => obj.ID == id);
        }
        public Siga_profs FindByUser(string user, string pass)
        {
            return  _context.Siga_profs.Where(obj => obj.user_login == user && obj.user_pass == pass).FirstOrDefault();
        }




        
        private bool Siga_profsExists(int id)
        {
            return _context.Siga_profs.Any(e => e.ID == id);
        }
    }
}
