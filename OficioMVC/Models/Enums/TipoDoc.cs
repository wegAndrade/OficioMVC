using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Models.Enums
{
    public enum TipoDoc : int
    {
        Edital = 0,
        [Display(Name = "Edital Interno")]
        EditalInterno = 1,
        [Display(Name = "Edital Externo")]
        EditalExterno = 2,
        Oficio = 3,
        Memorando = 4,
        Portaria = 5,

    }

    
}

