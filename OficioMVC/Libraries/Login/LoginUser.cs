using Newtonsoft.Json;
using OficioMVC.Models;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Libraries.Login
{
    public class LoginUser
    {
        private string Key = "Login";
        private Sessao.Sessao _sessao;

        public LoginUser(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }
        public void Login(Siga_profs usuario)
        {
            //Serializar
            string clienteJSONString = JsonConvert.SerializeObject(usuario);

            _sessao.Cadastrar(Key, clienteJSONString);
        }
        public Siga_profs GetUser()
        {
            //Deserializar
            if (_sessao.Existe(Key))
            {
                string clienteJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Siga_profs>(clienteJSONString); ;
            }
            else
            {
                return null;
            }
        }

        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
