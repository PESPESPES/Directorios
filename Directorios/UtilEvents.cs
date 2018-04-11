using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directorios
{
    public class UtilEvents
    {
        public event Action<int> ProgressChanged;

        private void OnProgressChanged(int progress)
        {
            var eh = ProgressChanged;
            if (eh != null)
            {
                eh(progress);
            }
        }
        public void Progres(int progress)
        {
            OnProgressChanged(progress);
        }
    }
}
