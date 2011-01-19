using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    public class Matrix : IFrameReceiver
    {
        private RoutingTable routingTable = new RoutingTable(); //tworzymy RouteTable z HashTable
        public RoutingTable RoutingTable { get { return routingTable; } }

        private Switch node;

        public Matrix(Switch node)
        {
            this.node = node;
        }

        // Tutaj odbywa sie wlasciwa komutacja - przychodzaca ramka jest modyfikowana 
        // i przekazywana na odpowiedni port wyjsciowy
        public void ReceiveFrame(ProtocolUnit pu, int port)
        {
            // Wczytanie portu, VPI i VCI z ramki
            RoutingEntry source = new RoutingEntry(port, pu.Vpi, pu.Vci);
            if (routingTable.ContainsKey(source))
            {
                // Znalezienie odpowiadajacego wczytanym danym wpisu w tabeli routingu
                RoutingEntry target = routingTable[source];
                // Modyfikacja ramki - VCI nie jest modyfikowane w przypadku agregacji sciezek
                pu.Vpi = target.Vpi;
                if (!target.NoVci)
                    pu.Vci = target.Vci;
                // Wyslanie ramki na odpowiedni port wyjsciowy
                node.Log.LogMsg("Ramka: " + source.ToString() + " -> " + target.ToString());
                node.PortsOut[target.Port].Send(pu);
            }
            else
                node.Log.LogMsg("Ramka: " + source.ToString() + " odrzucona.");
        }
    }
}
