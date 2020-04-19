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

            Documento documento = new Documento(1, 1, 2020, "Teste de desenvolvimento para criação de ambiente de homologação","Observação de teste", teste,Models.Enums.TipoDoc.Edital,DateTime.Now );
            Documento documento1 = new Documento(2, 2, 2020, "Sou um Edital do Ambiente de desenvolvimento", "Observação de Desenvolvimento", teste, Models.Enums.TipoDoc.Edital, DateTime.Now);
            Documento documento2 = new Documento(3, 3, 2020, "Sou um Edital Externo do Ambiente de desenvolvimento", "Observação de Desenvolvimento", teste, Models.Enums.TipoDoc.EditalExterno, DateTime.Now);
            Documento documento3 = new Documento(4, 4, 2020, "Sou um Edital Interno do Ambiente de desenvolvimento", "Observação de Desenvolvimento", teste, Models.Enums.TipoDoc.EditalInterno, DateTime.Now);
            Documento documento4 = new Documento(5,  5, 2020, "Sou um Oficio do Ambiente de desenvolvimento", "Observação de Desenvolvimento", teste, Models.Enums.TipoDoc.Oficio, DateTime.Now);
            Documento documento5 = new Documento(6, 6, 2020, "Sou um Memorando  do Ambiente de desenvolvimento", "Observação de Desenvolvimento", teste, Models.Enums.TipoDoc.Memorando, DateTime.Now);
            Documento documento6 = new Documento(7, 7, 2020, "Sou uma Portaria  do Ambiente de desenvolvimento", "Observação de Desenvolvimento", teste, Models.Enums.TipoDoc.Portaria, DateTime.Now);
            
            _context.Documento.AddRange(documento,documento1,documento2,documento3,documento4,documento5,documento6);

            _context.SaveChanges();

        }
    }
}
