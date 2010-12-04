using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class PortOut
    {        // reprezancacja portu wyjściowego, realizuje przekazanie danych do portu po drugiej stronie łącza 


        private int number; //numer identyfikujacy port wyjsciowy.

        public int GetNumber() { return number; }

        public void SetNumber(int i) { number = i; }

        public void Send(ProtocolUnit pu) { }  // metoda ktora bedzie wysylala ProtocolUnit ktory jej podamy na wejscie.


    }
}
