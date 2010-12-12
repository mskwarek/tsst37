using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim.Components
{
    public class Data
    {
        private string id;  // czyli wlasciwie wysylana wiadomosc ,jest ona np postaci "DGWDDF" zawsze w postaci duzych liter alfabetu 
        //te litery reprezentuja 48Bajtow kazda
        private int length; //reprezentuje dlugosc wiadomosci

        private Random rand = new Random(); //zmienna losowa do generowania losowej wiadomosci

        public int GetLength()
        { return id.Length; }

        public string GetId()
        { return id; }

        public string GetElement(int number)  //zwraca podany numer kawalka wiadomosci (jedna litera)
        { return id.Substring(number - 1, 1); }

        public void SetId(string i)
        { id = i; length = id.Length; }

        public string SetRandomData(int maxlength)    //generuje wiadomosc losowa o losowej dlugosci i zawartosci..sprawdzalem i dziala
        {
            id = "";
            int len = (length = rand.Next(maxlength));   //losowa dlugosc wiadomosci
            while (len > 0)
            {
                len--;
                char c = (char)(65 + rand.Next(25));   //tu mamy 25 bo liter alfabetu jest 26...65 to "A"  w ASCII

                id += c;
            }
            return id;
        }
    }
}
