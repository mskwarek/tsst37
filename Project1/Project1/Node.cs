using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    public class Node : AtmSim.Common.INetworkNode
    {    //pojedynczy węzeł sieci, nie generuje ruchu, tylko go kieruje

        private Agent a;

        public AtmSim.Common.IAgent Agent
        {
            get { return a; }
        }

        private AtmSim.Common.Log log;

        public AtmSim.Common.Log Log
        {
            get { return log; }
        }

        private string name = "unknown";

        private IPortIn[] inportsgroup; //porty wejsciowe wezla

        private IPortOut[] outportsgroup; //porty wyjsciowe wezla

        private Matrix tab; //= new Matrix();  //Matrix wezla czyli tablica routingu

        public string GetName() { return name; }   //zwraca imie wezla

        public void SetName(string s) { name = s; }  //ustawia imie wezla

        public IPortIn[] GetPortsIn() { return inportsgroup; } //metoda zwraca porty wejsciowe

        public IPortOut[] GetPortsOut() { return outportsgroup; }  //metoda zwraca porty wyjsciowe

        public void SetPortsIn(IPortIn[] pi) { inportsgroup = pi; } //metoda ustawia porty wejsciowe

        public void SetPortsOut(IPortOut[] po) { outportsgroup = po; } //metoda ustawia porty wyjsciowe

        public Matrix GetMatrix() { return tab; }  //zwraca Matrix-pole komutacyjne węzla

        public void SetMatrix(Matrix m) { tab = m; } //Ustawienie pola kom. dla węzla


        public Node(int numberofin, int numberofout, string name)
        {

            this.name = name;

            inportsgroup = new TestPortIn[numberofin];
            outportsgroup = new TestPortOut[numberofout];
            tab = new Matrix(this);

            for (int i = 0; i < inportsgroup.Length; i++)
            {
                TestPortIn port = new TestPortIn(i);
                port.SetReceiver(this.tab);
                inportsgroup[i] = port;
            }
            for (int j = 0; j < outportsgroup.Length; j++) { outportsgroup[j] = new TestPortOut(j); }


            this.log = new AtmSim.Common.Log("Log urzadzenia " + name);


            this.a = new Agent(this);
        }





        /*
         Metoda ChecInPorts najpierw poszukuje w puli portow wejsciowych ktore naleza do wezla tego na ktory wszedl pakiet .
         * Nastepnie szukamy odpowiadajacego temu pakietowi klucza w Tablicy Routingu. Klucz ten to  "numerInPort:wejscioweVPI:wejscioweVCI".
         * Na podstawie tego klucza bierzemy odpowiadajaca mu wartosc "numerOutPort:wyjscioweVPI:wyjscioweVCI" i  "wyławiamy" 
         * numer portu wyjsciowego,vpi wyjsciowe i vpi wyjsciowe (Zamieniamy je na int).
         * Pakietowi zamieniami numery vpi vci tak jak to wynika z tablicy routingu i znajdujemy w puli portow wyjsciowych port odpowiadajacy znalezionemu w
         * tablicy numerowi identyfikacyjnemu portu.
         * Ostatecznie wysylamy pakiet znalezionym portem wyjsciowym. 
         */

        //public void CheckInPorts()
        //{

        //    // tym trzem zmiennym bedziemy przypisywac numer portu wyjsciowego,vpi i vci wyjsciowe
        //    ///int portnum=new int();
        //    /// int vpi=new int();
        //    /// int vci=new int();


        //    foreach (PortIn p in inportsgroup)
        //    { //ta petla przeszukuje pule portow wejsciowych w celu znalezienia tego ktory ma pakiet do obsluzenia

        //        if (p.GetIsReceived() == true)
        //        {    //przechodzimy przez ten if gdy znajdziemy port z pakietem do obsluzenia               
        //            Common.RoutingEntry me = new Common.RoutingEntry(p.GetNumber(), p.GetProtocolUnit().GetVPI(), p.GetProtocolUnit().GetVCI());
        //            foreach (var de in tab.GetRouteTable())//przeszukujemy tablice routingu..
        //            {
        //                //az napotkamy wpis odpowiadajacy kluczowi utworzonemu z vci,vpi pakietu i z numeru portu wejsciowego.(wtedy mamy if(true))
        //                if (((Common.RoutingEntry)de.Key).Equals(me))
        //                {
        //                    me = (Common.RoutingEntry)de.Value;//odczytujemy wartosc tablicy routingu 


        //                    //w ponizszych trzech wierszach otrzymujemy vci,vpi i numer portu wyjsciowego w postaci int
        //                    ///portnum = int.Parse(s.Substring(0, s.IndexOf(":")));
        //                    /// vpi = int.Parse(s.Substring(s.IndexOf(":") + 1, s.LastIndexOf(":") - s.IndexOf(":") - 1));
        //                    ///vci = int.Parse(s.Substring(s.LastIndexOf(":") + 1, s.Length - s.LastIndexOf(":") - 1));

        //                    // wpisujemy w pakiecie ktory znalezlismy w jednym z portow wejsciowych nowe vci i vpi z tablicy routingu
        //                    p.GetProtocolUnit().SetVPI(me.Vpi);
        //                    p.GetProtocolUnit().SetVCI(me.Vci);

        //                    break;   //zeby nie przechodzic juz przez tablice jak znajdziemy wpis
        //                }

        //            }


        //            // W ponizszej petli szukamy w puli portow wyjsciowych wezla port ktorym mamy wyslac pakiet i go wysylamy
        //            foreach (PortOut pp in outportsgroup)
        //            {
        //                if (pp.GetNumber() == me.Port)
        //                { pp.Send(p.GetProtocolUnit()); }
        //            }

        //        }
        //    }



        //}




    }
}