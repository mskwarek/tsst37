using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    //generyczne źródło ruchu, wysyłające przez swój port dane o losowej długości 
    public class Sorce : Common.INetworkNode
    {
        public string Name;
        private Common.Log log;
        public Common.Log Log { get { return log; } set { log = value; } }

        private SorceAgent agent;
        public Common.IAgent Agent
        {
            get { return agent; }
        }

        private string message = "";
        private string target = "";
        public string Message { get { return message; } set { message = value; } }
        public string Target { get { return target; } set { target = value; } }

        private Common.RoutingTable targets = new Common.RoutingTable();
        public Common.RoutingTable Matrix {
            get { return targets; }
            set { targets = value; }
        }

        private AdaptationLayer aal = new AdaptationLayer();  //czyli AAL.Za pomoca tej klasy bedziemy mapowac strumien uzytkowy ktory sobie tez tu utworzymy

        private IPortOut firstport; //jedyny port wyjsciowy ktory jest w tej klasie do wysylania ProtocolUnitow.

        public Sorce(string name)
        {
            this.Name = name;
            this.log = new Common.Log("Log źródła " + name);
            agent = new SorceAgent(this);
        }

        public void SetPortOut(IPortOut po) { firstport = po; } //skojarzenie Sorcea z odpowiednim portem wyjsciowym

        public IPortOut GetPortOut() { return firstport; } //metoda zwraca port skojarzony z Klasa Sorce


        /*jezeli wybierzemy w metodzie GenerateData tryb losowego generowania strumienia uzytkowego to to bedzie maxymalna mozliwa
        dlugosc wylosowanego strumienia. Ta dlugosc ustawia sie poczatkowo automatycznie na wartosc 1000.
         */
        private int maximumlength = 1000;

        public void SetMaximumLength(int len) { maximumlength = len; } //Ustawianie maxymalnej dlugosci generowanego strumieni uzytkowego

        public int GetMaximumLength() { return maximumlength; } //Zwracanie maxymalnej dlugosci generowanego strumieni uzytkowego

        /*
         * Metoda GenerateData generuje losowy badz wybrany strumien uzytkowy.Jezeli wartoscia wejsciowa "string dataid" metody bedzie null to metoda stwierdzi 
         * ze sama musi wygenerowac dane ,jezeli natomiast podamy ten parametr to zostanie utworzony strumien w takiej wlasnie postaci ("dataid").
         * int firstvpi i int firstvci to vci i vpi ktore przypisujemy pakietą utworzonym z wygenerowanego strumienia uzytkowego(wszystkim pakietą z tego strumienia).
         * Metoda zwraca tablice pakietow ktora zostaje utworzona z wygenerowanego strumienia uzytkowego.
         */
        public ProtocolUnit[] GenerateData(string dataid, int firstvpi, int firstvci)
        {

            Data dat = new Data();    //tworzymy obiekt data....

            //ProtocolUnit pu = new ProtocolUnit();    //....i ProtocolUnit.

            if (dataid == null) { dat.SetRandomData(maximumlength); }       //sprawdzamy czy mamy wygenerowac losowo strumien uzytkowy ....
            else { dat.SetId(dataid); }         //....czy tez tworzymy go z wprowadzonego w metodzie stringa.
            ProtocolUnit[] putab = new ProtocolUnit[dat.GetLength()]; //to bedzie tablica pakietow utworzonych ze strumienia.

            aal.SetDataToMap(dat);   //do obiektu klasy AdaptacionLayer wstrzykujemy wygenerowany strumien....

            DataUnit[] du = aal.MapData(); //.... a tu pocinamy ten strumien niczym pietruszke i mamy zwracaną tablice DataUnitow
            int count = 0;  //licznik

            /* w ponizszej petli kazdy DataUnit utworzony ze strumienia przeksztalcamy na pakiet dodajac vci,vpi ktore podalismy na poczatku matody a 
             * nastepnie wysylamy ten pakiet portem poczatkowym.
             */


            foreach (DataUnit p in du)
            {
                var pu = new ProtocolUnit();
                pu.SetDataUnit(p);
                pu.SetVPI(firstvpi);
                pu.SetVCI(firstvci);
                putab[count] = pu; //to jest tylko po to zeby metoda zwracala tablice utworzonych pakietow ze strumienia uzytkowego.
                this.firstport.Send(pu);
                Log.LogMsg("Wysłano " + p.GetId());
            }



            return putab;
        }

        public void Send()
        {
            Common.RoutingEntry target = new Common.RoutingEntry(Matrix[this.Target]);
            
            GenerateData(Message, target.Vpi, target.Vci);
        }

    }
}
