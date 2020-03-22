using OficioMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Service.Seed
{
    public class SeedingService
    {
        private OficioMVCContext _context;

        public SeedingService(OficioMVCContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            if(_context.Documento.Any() || _context.Siga_profs.Any())
            {
                return; //DB está em uso
            }
            Siga_profs teste = new Siga_profs(1, "teste", "698dc19d489c4e4db73e28a713eab07b", "Teste", "ATIVO", "Administracao");
            _context.Siga_profs.Add(teste);

            Documento documento = new Documento(1, 1, 2020, "Teste de desenvolvimento para criação de ambiente de homologação","Observação de teste", teste,Models.Enums.TipoDoc.Edital,1,DateTime.Now );
            _context.Documento.Add(documento);

            

            _context.SaveChanges();

        }
    }
}
