using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Matrix
    {

        private Hashtable RouteTable = new Hashtable(); //tworzymy RouteTable z HashTable

        /*Gdy chcemy dodac cos nowego do Tablicy Routingowej to uzywamy AddToMatrix dajac odpowiednie porty i numery vci,vpi wejsciowe/wyjsciowe
         * otrzymywany string s1 to wspomniany wczesniej "numerInPort:wejscioweVPI:wejscioweVCI" s2 to "numerOutPort:wyjscioweVPI:wyjscioweVCI".
         * 
         */


        public void AddToMatrix(PortIn pi, int vpiin, int vciin, PortOut po, int vpiout, int vciout)
        {
            string s1 = pi.GetNumber().ToString() + ":" + vpiin.ToString() + ":" + vciin.ToString();

            string s2 = po.GetNumber().ToString() + ":" + vpiout.ToString() + ":" + vciout.ToString();

            if (RouteTable.ContainsKey(s1) || RouteTable.ContainsValue(s2)) return;  //Sprawdzamy czy mozemy wpisac do tablicy wiersz ktory podalismy w parametrach metody.
            RouteTable.Add(s1, s2); //Ostatecznie tworzymy nowy wiersz w tablicy z odpowiednio kluczem s1 i wartoscia s2.
        }
        /*W metodzie DeleteFromMatrix dajemy na wejsciu odpowiedni Klucz "numerInPort:wejscioweVPI:wejscioweVCI"
          A nastepnie metoda usowa wiersz o takim kluczu
         */


        public void DeleteFromMatrix(string key)
        {
            if (RouteTable.ContainsKey(key))
            {
                RouteTable.Remove(key);
            }


        }

        // GetRouteTable poprostu zwraca tablice taka jaka jest w obecnym stanie
        public Hashtable GetRouteTable() { return RouteTable; }


    }
}
