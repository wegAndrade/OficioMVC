using Microsoft.EntityFrameworkCore;
using OficioMVC.Models;
using OficioMVC.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Service
{
    public class EditalService
    {

        private readonly OficioMVCContext _context;
        public EditalService(OficioMVCContext context)
        {
            _context = context;
        }

        public async Task <Edital> FindByNumAsyn(int numeracao, int ano)
        {
            return await _context.Edital.Include(user =>user.Usuario).FirstOrDefaultAsync(x => x.Ano == ano && x.Numeracao == numeracao);
        }
        public async Task<List<Edital>> FindAllAsync()
        {
            return await _context.Edital.ToListAsync();
        }
        public  async Task InsertAsync(Edital obj)
        {
            
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int Numeracao, int ano)
        {
            try
            {
                var obj = _context.Edital.Find(Numeracao, ano);
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não podemos deletar esse edital ele possui dados filhos!");
            }

        }
        public async Task UpdateAsync(Edital obj)
        {
            bool HasAny = await _context.Edital.AnyAsync(x => x.Numeracao == obj.Numeracao && x.Ano == obj.Ano);
            if (!HasAny)
            {
                throw new NotFoundException("Edital não encontrado");
            }
            try
            {
                _context.Edital.Update(obj);
                await _context.SaveChangesAsync();


            }catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurencyException(e.Message);
            }
        }
 
    }
}
