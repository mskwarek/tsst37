using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{

    class PortIn
    {               //reprezentacja portu wejściowego, realizuje otrzymywanie danych 

        private int number;      //numer identyfikujacy port wejsciowy

        private ProtocolUnit protuni;

        private bool isreceived = false;    //wartosc logiczna okreslajaca czy wezel ma do obsluzenia(mapowanie w poku kom.) pakiet.Jezeli tak to "true" jezeli nie to "false".

        public int GetNumber() { return number; }

        public void SetNumber(int i) { number = i; }

        public void Receive(ProtocolUnit pu) { protuni = pu; isreceived = true; } //otrzymanie pakietu przez port wejsciowy i zamiana wartosci logicznej na "true".

        public bool GetIsReceived() { return isreceived; }  //tu poprostu pobieramy wartosc  logiczna isreceived.

        public void SetIsReceived(bool b) { isreceived = b; }// metoda dla wezla. Po obsluzeniu pakietu musi ustawic z powrotem wartosc logicza false.

        public ProtocolUnit GetProtocolUnit() { return protuni; } //zwaca protocol unit
    }







}
