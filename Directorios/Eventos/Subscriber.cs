using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Directorios.Eventos.ErroresDatos;

namespace Directorios.Eventos
{
    public class Subscriber
    {
        private string id;
        public Subscriber(string ID, ErroresDatos pub)
        {
            id = ID;
            // Subscribe to the event using C# 2.0 syntax
            pub.ThresholdReached += HandleCustomEvent;
        }

        // Define what actions to take when the event is raised.
        void HandleCustomEvent(object sender, ThresholdReachedEventArgs e)
        {
            Console.WriteLine(id + " received this message: {0}", e.ErrorDescription);
        }
    }

}
