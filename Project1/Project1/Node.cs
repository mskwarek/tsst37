using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{

    class Node
    {    //pojedynczy węzeł sieci, nie generuje ruchu, tylko go kieruje

        private string name;

        private PortIn[] inportsgroup;  //porty wejsciowe wezla

        private PortOut[] outportsgroup; //porty wyjsciowe wezla

        private Matrix tab;  //Matrix wezla czyli tablica routingu

        public string GetName() { return name; }   //zwraca imie wezla

        public void SetName(string s) { name = s; }  //ustawia imie wezla

        public PortIn[] GetPortsIn() { return inportsgroup; } //metoda zwraca porty wejsciowe

        public PortOut[] GetPortsOut() { return outportsgroup; }  //metoda zwraca porty wyjsciowe

        public void SetPortsIn(PortIn[] pi) { inportsgroup = pi; } //metoda ustawia porty wejsciowe

        public void SetPortsOut(PortOut[] po) { outportsgroup = po; } //metoda ustawia porty wyjsciowe

        public Matrix GetMatrix() { return tab; }  //zwraca Matrix-pole komutacyjne węzla

        public void SetMatrix(Matrix m) { tab = m; } //Ustawienie pola kom. dla węzla


        /*
         Metoda ChecInPorts najpierw poszukuje w puli portow wejsciowych ktore naleza do wezla tego na ktory wszedl pakiet .
         * Nastepnie szukamy odpowiadajacego temu pakietowi klucza w Tablicy Routingu. Klucz ten to  "numerInPort:wejscioweVPI:wejscioweVCI".
         * Na podstawie tego klucza bierzemy odpowiadajaca mu wartosc "numerOutPort:wyjscioweVPI:wyjscioweVCI" i  "wyławiamy" 
         * numer portu wyjsciowego,vpi wyjsciowe i vpi wyjsciowe (Zamieniamy je na int).
         * Pakietowi zamieniami numery vpi vci tak jak to wynika z tablicy routingu i znajdujemy w puli portow wyjsciowych port odpowiadajacy znalezionemu w
         * tablicy numerowi identyfikacyjnemu portu.
         * Ostatecznie wysylamy pakiet znalezionym portem wyjsciowym. 
         */

        public void CheckInPorts()
        {

            // tym trzem zmiennym bedziemy przypisywac numer portu wyjsciowego,vpi i vci wyjsciowe
            int portnum = new int();
            int vpi = new int();
            int vci = new int();


            foreach (PortIn p in inportsgroup)
            { //ta petla przeszukuje pule portow wejsciowych w celu znalezienia tego ktory ma pakiet do obsluzenia

                if (p.GetIsReceived() == true)
                {    //przechodzimy przez ten if gdy znajdziemy port z pakietem do obsluzenia               

                    foreach (DictionaryEntry de in tab.GetRouteTable())//przeszukujemy tablice routingu..
                    {
                        //az napotkamy wpis odpowiadajacy kluczowi utworzonemu z vci,vpi pakietu i z numeru portu wejsciowego.(wtedy mamy if(true))
                        if ((string)de.Key == p.GetNumber().ToString() + ":" + p.GetProtocolUnit().GetVPI().ToString() + ":" + p.GetProtocolUnit().GetVCI().ToString())
                        {
                            string s = (string)de.Value;//odczytujemy wartosc tablicy routingu 


                            //w ponizszych trzech wierszach otrzymujemy vci,vpi i numer portu wyjsciowego w postaci int
                            portnum = int.Parse(s.Substring(0, s.IndexOf(":")));
                            vpi = int.Parse(s.Substring(s.IndexOf(":") + 1, s.LastIndexOf(":") - s.IndexOf(":") - 1));
                            vci = int.Parse(s.Substring(s.LastIndexOf(":") + 1, s.Length - s.LastIndexOf(":") - 1));

                            // wpisujemy w pakiecie ktory znalezlismy w jednym z portow wejsciowych nowe vci i vpi z tablicy routingu
                            p.GetProtocolUnit().SetVPI(vpi);
                            p.GetProtocolUnit().SetVCI(vci);

                            break;   //zeby nie przechodzic juz przez tablice jak znajdziemy wpis
                        }

                    }


                    // W ponizszej petli szukamy w puli portow wyjsciowych wezla port ktorym mamy wyslac pakiet i go wysylamy
                    foreach (PortOut pp in outportsgroup)
                    {
                        if (pp.GetNumber() == portnum)
                        { pp.Send(p.GetProtocolUnit()); }
                    }

                }
            }



        }




    }

}
