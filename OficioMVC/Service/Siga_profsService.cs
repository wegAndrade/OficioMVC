using Microsoft.EntityFrameworkCore;
using OficioMVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OficioMVC.Models;

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
        public async Task<Siga_profs> FindByUserAndPassAsync(string user, string pass)
        {
            return await _context.Siga_profs.FirstOrDefaultAsync(obj => obj.user_login == user && obj.user_pass == pass);
        }
    }
}
