using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directorios
{
    public class Errores
    {
        private ConcurrentBag<string> erroresData = new ConcurrentBag<string>();
        public ConcurrentBag<string> ErroresData { get => erroresData; set => erroresData = value; }

    }
}
