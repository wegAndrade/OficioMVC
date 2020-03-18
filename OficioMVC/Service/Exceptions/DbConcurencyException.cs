using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OficioMVC.Service.Exceptions
{
    public class DbConcurencyException: ApplicationException
    {
        public DbConcurencyException(string massage): base(massage)
        {

        }
    }
}
