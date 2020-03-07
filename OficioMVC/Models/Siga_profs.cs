using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OficioMVC.Models.Enums.Departamento;

namespace OficioMVC.Models
{
    public class Siga_profs
    {
        
            public int ID { get; set; }
            public string user_login { get; set; }
            public string user_pass { get; set; }
            public string user_nicename { get; set; }
            public int ativo { get; set; }
            public departamento dpto { get; set; }
        }
    }



