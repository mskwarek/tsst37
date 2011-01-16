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

        /*Gdy chcemy dodac cos nowego do Tablicy Routingowej to uzywamy AddToMatrix dajac odpowiednie porty i numery vci,vpi wejsciowe/wyjsciowe
         * otrzymywany string s1 to wspomniany wczesniej "numerInPort:wejscioweVPI:wejscioweVCI" s2 to "numerOutPort:wyjscioweVPI:wyjscioweVCI".
         * 
         */
        private Node node;

        public Matrix(Node node)
        {
            this.node = node;
        }

        public void AddToMatrix(RoutingEntry me1, RoutingEntry me2)
        {
            if (routingTable.ContainsKey(me1) || routingTable.ContainsValue(me2)) return;  //Sprawdzamy czy mozemy wpisac do tablicy wiersz ktory podalismy w parametrach metody.
            routingTable.Add(me1, me2); //Ostatecznie tworzymy nowy wiersz w tablicy z odpowiednio kluczem s1 i wartoscia s2.
        }

        /*W metodzie DeleteFromMatrix dajemy na wejsciu odpowiedni Klucz "numerInPort:wejscioweVPI:wejscioweVCI"
          A nastepnie metoda usuwa wiersz o takim kluczu
         */
        public void DeleteFromMatrix(RoutingEntry me)
        {
            if (routingTable.ContainsKey(me))
            {
                routingTable.Remove(me);
            }
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
