using Microsoft.EntityFrameworkCore;
using OficioMVC.Models;
using OficioMVC.Models.Enums;
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
        //Selecionar por ID
        public async Task<Documento> FindById(int? id)
        {
            var documento = await _context.Documento.AsNoTracking()
            .Include(d => d.Usuario)
            .FirstOrDefaultAsync(m => m.Id == id);
            _context.SaveChanges();

            return documento;
        }
        //Verificar se o documento Existe
        public bool DocumentoExists(int id)
        {
            return _context.Documento.Any(e => e.Id == id);
        }
        //Selecionar todos documentos com exceção dos excluidos com base no parametro
        public async Task<List<Documento>> FindAllAsync(bool exc)
        {
            if (exc)
            {
                return await _context.Documento.Include(d => d.Usuario).Where(d => d.Status != StatusDoc.Excluido).OrderByDescending(d => d.DataEnvio).ToListAsync();
            }
            return await _context.Documento.Include(d => d.Usuario).OrderByDescending(d => d.DataEnvio).ToListAsync();

        }
        //inserir documentos de forma assincrona
        public async Task InsertAsync(Documento obj)
        {

            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        //atualizar de forma assincrona
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
        //Selecionar a ultima númeração usada para um documento
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

        //Retorna o caminho do arquivo do documento
        public async Task<string> GetCaminhoArq(int id)
        {
            var documento = await  FindById(id);
            
            return  documento.CaminhoArq;
        }
        //Seleciona todos os tipos de documentos
        public List<TipoDoc> GetAllTypes()
        {
            List<TipoDoc> Tipos = new List<TipoDoc>();
            foreach (TipoDoc i in Enum.GetValues(typeof(TipoDoc)))
            {
                Tipos.Add(i);
            }
            return Tipos;

        }
  
        public async Task<List<Documento>> FindByDataEDepartamento(String departamento, DateTime? min, DateTime? max)
        {
            var result = from obj in _context.Documento select obj;
            if (min.HasValue)
            {
                result = result.Where(x => x.DataEnvio >= min.Value);
            }
            if(max.HasValue)
            {
                result = result.Where(x => x.DataEnvio <= max.Value);
            }
            if(departamento != "" && departamento != null)
            {
                result = result.Where(x => x.Usuario.dpto == departamento);
            }

            return await result.Include(d => d.Usuario).Where(d => d.Status != StatusDoc.Excluido).OrderByDescending(x=> x.DataEnvio).ToListAsync();
            

      
        }
        public async Task<List<Documento>> FindByAssuntoObservacoes(string busca)
        {
           return await  _context.Documento.Where(d => d.Assunto.Contains(busca) || d.Observacoes.Contains(busca)).OrderByDescending(x => x.DataEnvio).ToListAsync();

            
        }
        

    }
}
