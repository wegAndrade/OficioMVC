using OficioMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Models.ViewModels
{
    public class DocumentoViewModel
    {
        public Siga_profs Usuario { get; set; }
        public Documento Documento { get; set; }
        public List<TipoDoc> Tipos { get; set; } = new List<TipoDoc>();



        public void teste()
        {
            //
            var type= Tipos[0];
                for(int i =0; i < Tipos.Count; i++)
            {
                type = Tipos[i];
                Console.WriteLine(type);
            }
                foreach (TipoDoc name in Tipos)
                {
                    Console.WriteLine(name.ToString());
                }
        
            
           

        }
      
    }
}
