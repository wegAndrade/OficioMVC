using Microsoft.EntityFrameworkCore;
using OficioMVC.Models;
using OficioMVC.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Service
{
    public class DocumentoService
    {
        private readonly OficioMVCContext _context;
        public DocumentoService(OficioMVCContext context)
        {
            _context = context;
        }

        public async Task<Documento> FindByNumAsyn(int numeracao, int ano)
        {
            return await _context.Documento.Include(user => user.Usuario).FirstOrDefaultAsync(x => x.Ano == ano && x.Numeracao == numeracao);
        }
        public async Task<Documento> FindById(int id)
        {
            var documento = await _context.Documento
            .Include(d => d.Usuario)
            .FirstOrDefaultAsync(m => m.Id == id);

            return documento;
        }
        public async Task<List<Documento>> FindAllAsync()
        {
            return await _context.Documento.ToListAsync();
        }
        public async Task InsertAsync(Documento obj)
        {

            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int Numeracao, int ano)
        {
            try
            {
                var obj = _context.Documento.Find(Numeracao, ano);
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Não podemos deletar esse Documento ele possui dados filhos!");
            }

        }
        public async Task UpdateAsync(Documento obj)
        {
            bool HasAny = await _context.Documento.AnyAsync(x => x.Numeracao == obj.Numeracao && x.Ano == obj.Ano);
            if (!HasAny)
            {
                throw new NotFoundException("Documento não encontrado");
            }
            try
            {
                _context.Documento.Update(obj);
                await _context.SaveChangesAsync();


            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurencyException(e.Message);
            }
        }
        public int GetNumMax()
        {
            int Ano = DateTime.Now.Year;
            
            var max1 = _context.Documento.Where(x => x.Ano == Ano).ToList();
            if(max1.Count == 0)
            {
                return 1;
            }
            var max = max1.Max(x => x.Numeracao);
            return max + 1;
        }

        public string GetCaminhoArq(int id)
        {

            var documento = _context.Documento.AsNoTracking()
            .FirstOrDefault(m => m.Id == id);
            _context.SaveChanges();
            return  documento.CaminhoArq;
        }
        
    }
}
