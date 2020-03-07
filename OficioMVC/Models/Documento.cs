using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Models
{
    public class Documento
    {
     

        public int Id { get; set; }
        public int Numeracao { get; set; }
        public int Ano { get; set; }
        public string Assunto { get; set; }
        public string Observacoes { get; set; }
        public string CaminhoArq { get; set; }
        public DateTime DataEnvio { get; set; }
        public Siga_profs Usuario { get; set; }



    }
}

