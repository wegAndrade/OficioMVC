using Newtonsoft.Json;
using OficioMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OficioMVC.Service
{
    public class LoginService
    {
        public static Siga_profs Obter(ClaimsPrincipal user)
        {
            var userData = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.UserData);
            if (userData != null)
                return JsonConvert.DeserializeObject<Siga_profs>(userData.Value);

            return null;
        }

        public Siga_profs ObterSimples(ClaimsPrincipal user)
        {
            return Obter(user);
        }

    }
}
