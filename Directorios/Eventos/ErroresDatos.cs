using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directorios.Eventos
{
    /// <summary>
    /// Evento Para Capturar Errores
    /// </summary>
    public class ErroresDatos
    {
        public event ThresholdReachedEventHandler ThresholdReached;
        public delegate void ThresholdReachedEventHandler(object sender, ThresholdReachedEventArgs e);
        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            ThresholdReachedEventHandler handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public void AgregarDatos(string Ruta, String Error)
        {
            ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
            args.Archivo = Ruta;
            args.ErrorDescription = Error;
        }

        public class ThresholdReachedEventArgs : EventArgs
        {
            public string ErrorDescription { get; set; }
            public string Archivo { get; set; }
        }
    }
}
