using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Service.Exceptions
{
    public class ArquivoOcurrencyException: SystemException
    {
        public ArquivoOcurrencyException(string message): base(message)
        {

        }
    }
}
