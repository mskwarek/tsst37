using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
   public class Matrix : IFrameReceiver
    {

        private Hashtable RouteTable = new Hashtable(); //tworzymy RouteTable z HashTable

        /*Gdy chcemy dodac cos nowego do Tablicy Routingowej to uzywamy AddToMatrix dajac odpowiednie porty i numery vci,vpi wejsciowe/wyjsciowe
         * otrzymywany string s1 to wspomniany wczesniej "numerInPort:wejscioweVPI:wejscioweVCI" s2 to "numerOutPort:wyjscioweVPI:wyjscioweVCI".
         * 
         */
        private Node node;

        public Matrix(Node node)
        {
            this.node = node;
        }

        public void AddToMatrix(MatrixElements me1, MatrixElements me2)
        {

            //MatrixElements me1 = new MatrixElements(pi.GetNumber(), vpiin, vciin);

            // MatrixElements me2 = new MatrixElements(po.GetNumber(), vpiout, vciout);


            //  string s1 =pi.GetNumber().ToString() + ":" + vpiin.ToString() + ":" + vciin.ToString();

            // string s2 = po.GetNumber().ToString() + ":" + vpiout.ToString() + ":" + vciout.ToString();

            if (RouteTable.ContainsKey(me1) || RouteTable.ContainsValue(me2)) return;  //Sprawdzamy czy mozemy wpisac do tablicy wiersz ktory podalismy w parametrach metody.
            RouteTable.Add(me1, me2); //Ostatecznie tworzymy nowy wiersz w tablicy z odpowiednio kluczem s1 i wartoscia s2.
        }
        /*W metodzie DeleteFromMatrix dajemy na wejsciu odpowiedni Klucz "numerInPort:wejscioweVPI:wejscioweVCI"
          A nastepnie metoda usowa wiersz o takim kluczu
         */


        public void DeleteFromMatrix(MatrixElements me)
        {
            if (RouteTable.ContainsKey(me))
            {
                RouteTable.Remove(me);
            }


        }

        // GetRouteTable poprostu zwraca tablice taka jaka jest w obecnym stanie
        public Hashtable GetRouteTable() { return RouteTable; }

        public void ReceiveFrame(ProtocolUnit pu, int port)
        {
            MatrixElements target = (MatrixElements)RouteTable[new MatrixElements(port, pu.Vci, pu.Vpi)];
            pu.Vci = target.Vci;
            pu.Vpi = target.Vpi;
            node.GetPortsOut().ElementAt(target.Port).Send(pu);
        }

    }
}
