using OficioMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Libraries.Autenticacao
{
    public interface IAutenticacao
    {
        bool ValidarLogin(Siga_profs usuario);
    }
}
