using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Service.Exceptions
{
    public class IntegrityException: ApplicationException
    {
        public IntegrityException(string massage): base(massage)
        {

        }
    }
}
