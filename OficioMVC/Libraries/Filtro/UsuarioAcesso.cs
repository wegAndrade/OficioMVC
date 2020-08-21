using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OficioMVC.Libraries.Login;
using OficioMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Libraries.Filtro
{
    public class UsuarioAcesso: ActionFilterAttribute
    {
        LoginUser _loginUser;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
               
                Controller controller = filterContext.Controller as Controller;

                if (controller != null)
                {
                _loginUser = (LoginUser)filterContext.HttpContext.RequestServices.GetService(typeof(LoginUser));
                Siga_profs User = _loginUser.GetUser();
                if (User.master == false)
                    {
                        controller.HttpContext.Response.Redirect("/Home/About");
                    }
                }

            }

        }
    }

