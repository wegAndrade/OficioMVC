using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Service.Exceptions
{
    public class NotFoundException: ApplicationException
    {
        public NotFoundException(string massage): base(massage)
        {

        }
    }
}
