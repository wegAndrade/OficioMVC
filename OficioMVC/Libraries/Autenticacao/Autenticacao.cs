using OficioMVC.Models;
using OficioMVC.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Libraries.Autenticacao
{
    public class Autenticacao:IAutenticacao
    {
        private readonly Siga_profsService _profsService;
        public Autenticacao(Siga_profsService profsService)
        {
            _profsService = profsService;
        }

        public bool ValidarLogin(Siga_profs usuario)
        {
            HashPass md5 = new HashPass();
            string password = md5.GerarMD5(usuario.user_pass);
            var user = _profsService.FindByUser(usuario.user_login, password);
            if(user == null)
            {
                return false;
            }
            return true;
        }
    }
}
